using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileMapPC
{
    public class Map_GUI
    {

        private List<Water> waterTiles = new List<Water>();
        private List<Stone> stoneTiles = new List<Stone>();
        private List<Brick_GUI> brickTiles = new List<Brick_GUI>();
        private List<Blank> blankTiles = new List<Blank>();
        
        private int size;
        private int[,] contents;

        public int Size
        {
            get { return size; }
        }

        public int[,] Contents
        {
            get { return contents; }
        }

        public List<Water> WaterTiles
        {
            get { return waterTiles; }
        }

        public List<Stone> StoneTiles
        {
            get { return stoneTiles; }
        }

        public List<Brick_GUI> BrickTiles
        {
            get { return brickTiles; }
        }

        public List<Blank> BlankTiles
        {
            get { return blankTiles; }
        }

        private int width, height;

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Map_GUI() { }
        #region
        public void Generate(int[,] map, int size)
        {
            contents = map;
            this.size = size;
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number == 1) //Brick
                    {
                        brickTiles.Add(new Brick_GUI(new Rectangle(x * size, y * size, size, size)));
                        width = (x + 1) * size;
                        height = (y + 1) * size;
                    }
                    else if (number == 2) //Stone
                    {
                        stoneTiles.Add(new Stone(new Rectangle(x * size, y * size, size, size)));
                        width = (x + 1) * size;
                        height = (y + 1) * size;
                    }
                    else if (number == 3) //Water
                    {
                        waterTiles.Add(new Water(new Rectangle(x * size, y * size, size, size)));
                        width = (x + 1) * size;
                        height = (y + 1) * size;
                    }
                    else if (number == 0) //Blank
                    {
                        blankTiles.Add(new Blank(new Rectangle(x * size, y * size, size, size)));
                        width = (x + 1) * size;
                        height = (y + 1) * size;
                    } 

                }
            }
        }
        #endregion
        int count = 0;

        public void Update(GameTime gametime,DataStorage ds,Player_GUI[] pl,List<Coin_GUI> co,List<Lifepack_GUI> li)
        {
            int damage, x, y;
            Brick[] br = ds.getBricks();
            for (int i = 0; i < br.Length; i++)
            {
                x = br[i].getX();
                y = br[i].getY();
                damage = br[i].getDamage();
                var brick = brickTiles.First(d => d.Rectangle.X == (x * size) && d.Rectangle.Y == (y * size));
                brick.setDamageLevel(damage);
            }

            Player[] playerDi = ds.getPlayers();
            for (int i = 0; i < playerDi.Length; i++)
            {
                pl[i].setValues(playerDi[i].getX(), playerDi[i].getY(), playerDi[i].getDirection(),playerDi[i].getHealth());
            }

            List<CoinPile> c = ds.getCoinPiles();
            for (int i = 0; i < c.Count; i++)
            {
                CoinPile cc = c.ElementAt(i);
                if(cc.disappeared==false || cc.isCollected == false) co.Add(new Coin_GUI(cc));
            }

            List<LifePack> l = ds.getLifePacks();
            for (int i = 0; i < l.Count; i++)
            {
                LifePack ll = l.ElementAt(i);
                if (ll.disappeared == false || ll.isCollected == false) li.Add(new Lifepack_GUI(ll));
            }
                    
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Water tile in waterTiles)
                tile.Draw(spriteBatch);
            foreach (Stone tile in stoneTiles)
                tile.Draw(spriteBatch);
            foreach (Brick_GUI tile in brickTiles)
                tile.newDraw(spriteBatch);
            foreach (Blank tile in blankTiles)
                tile.Draw(spriteBatch);
        }
    }
}
