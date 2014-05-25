using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace TileMapPC
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        int screenWidth;
        int screenHeight;
        int[,] intGrid = new int[20,20];

        Map_GUI map;
        Player_GUI[] players = new Player_GUI[5];
        List<Coin_GUI> coins = new List<Coin_GUI>();
        List<Lifepack_GUI> lifepacks = new List<Lifepack_GUI>();
        Communicator com;
        ResponseHandler handler;
        Decision decision;
        public List<Bullet_GUI> bullets = new List<Bullet_GUI>();
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 640;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "TANK BATTLES";

            handler = new ResponseHandler();
            decision = new Decision();
            com = new Communicator();
            map = new Map_GUI();
            join();
            setGrid();
            Player[] playersD = handler.getDataStorage().getPlayers();
            Player p;
            for (int i = 0; i < 5; i++)
            {
                p = playersD[i];
                players[i] = new Player_GUI(p.getX(), p.getY(), p.getDirection(), p.getID());
            }
            base.Initialize();
        }

        #region
        public void join()
        {
            com.writeToServer("JOIN#");
            String s = com.readFromServer();
            if (s.Contains("I"))
            {
                handler.handle(s);

            }
            s = com.readFromServer();
            if (s.Contains("S"))
            {
                handler.handle(s);

            }
            //initilizing the decision
            Brick[] b = handler.getDataStorage().getBricks();
            Cell[] st = handler.getDataStorage().getStones();
            Cell[] w = handler.getDataStorage().getWater();
            decision.initialize(b, st, w);
        }

        public void setGrid()
        {
            String[,] arr = handler.getDataStorage().area.grid;
            String str;
            for(int i = 0; i<20;i++)
            {
                for (int j = 0; j < 20;j++ )
                {
                    str = arr[i,j];
                    if (str == "B") intGrid[i, j] = 1;
                    else if (str == "W") intGrid[i, j] = 3;
                    else if (str == "S") intGrid[i, j] = 2;
                    else if (str == "N") intGrid[i, j] = 0;
                }
            }
        }
        #endregion

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;

            backgroundTexture = Content.Load<Texture2D>("background");
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            Tiles_GUI.Content = Content;

            map.Generate(intGrid, 32);
            foreach(Player_GUI player in players){
                player.Load(Content);
            }
            font = Content.Load<SpriteFont>("TankFont");
            //lifepack.Load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        int count = 0;
        protected override void Update(GameTime gameTime)
        {
            
            if (count % 60 == 0)
            {
                
                String str = write();
                if (str == "SHOOT#")
                {
                    /*DataStorage d = handler.getDataStorage();
                    int p = d.getMyPlayer();
                    Player pl = (d.getPlayers())[p];
                    Bullet_GUI b = new Bullet_GUI();
                    b.isShot(new Vector2(pl.getX()*32,pl.getY()*32),pl.getDirection(),map);
                    b.Load(Content);
                    //b.Update(gameTime,map);
                    bullets.Add(b);*/
                }
                com.writeToServer(str);

                String s = com.readFromServer();
                handler.handle(s);
                map.Update(gameTime, handler.getDataStorage(), players , coins, lifepacks);

                base.Update(gameTime);
            }
            
            count++;
            
        }


        public String write()
        {
            Console.WriteLine("writing to server");
            //Random rnd = new Random();
            //int n = rnd.Next(1, 5);

            //get decision
            int n = decision.takeDecision(handler.getDataStorage());
            Console.WriteLine(n);
            if (n == 1)
            {
                return "UP#";
            }
            if (n == 2)
            {
                return "RIGHT#";
            }
            if (n == 3)
            {
                return "DOWN#";
            }
            if (n == 4)
            {
                return "LEFT#";
            }
            if (n == 5)
            {
                return "SHOOT#";
            }
            else
            {
                return null;
            }
        }
   
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        private void DrawText()
        {
            spriteBatch.DrawString(font, "Player ID", new Vector2(650, 160), Color.Black);
            spriteBatch.DrawString(font, "Coins", new Vector2(725, 160), Color.Black);
            spriteBatch.DrawString(font, "Points", new Vector2(800, 160), Color.Black);
            spriteBatch.DrawString(font, "Health", new Vector2(875, 160), Color.Black);
            spriteBatch.DrawString(font, "P0", new Vector2(650, 190), Color.Black);
            spriteBatch.DrawString(font, "P1", new Vector2(650, 220), Color.Black);
            spriteBatch.DrawString(font, "P2", new Vector2(650, 250), Color.Black);
            spriteBatch.DrawString(font, "P3", new Vector2(650, 280), Color.Black);
            spriteBatch.DrawString(font, "P4", new Vector2(650, 310), Color.Black);


            DataStorage ds = handler.getDataStorage();

            Player[] p = ds.getPlayers();
            //coins
            for (int i = 0; i < 5; i++)
            {
                spriteBatch.DrawString(font, p[i].getCoins().ToString(), new Vector2(725, 190 + 30*i), Color.Black);
            }

            //points
            for (int i = 0; i < 5; i++)
            {
                spriteBatch.DrawString(font, p[i].getPoints().ToString(), new Vector2(800, 190 + 30 * i), Color.Black);
            }

            //health
            for (int i = 0; i < 5; i++)
            {
                spriteBatch.DrawString(font, p[i].getHealth().ToString(), new Vector2(875, 190 + 30 * i), Color.Black);
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            map.Draw(spriteBatch);
            foreach (Player_GUI player in players)
            {
                player.Draw(spriteBatch);
            }
            foreach(Coin_GUI c in coins)
            {
                if (c != null) 
                {
                    c.Load(Content);
                    c.Draw(spriteBatch,coins,handler); 
                }
            }
            foreach(Lifepack_GUI c in lifepacks)
            {
                if (c != null) 
                {
                    c.Load(Content);
                    c.Draw(spriteBatch,lifepacks,handler); 
                }
            }
            foreach (Bullet_GUI b in bullets)
            {
                b.Draw(spriteBatch);
            }
            DrawText();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
