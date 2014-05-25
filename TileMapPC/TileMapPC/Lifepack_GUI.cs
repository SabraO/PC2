using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace TileMapPC
{
    public class Lifepack_GUI
    {
        private Texture2D texture;

        private Vector2 position;
        private Rectangle rectangle;

        private long timeLeftToDisappear;//in seconds
        private bool Collected;
        public LifePack lif;
        Timer t;
        int size = 32;

        public Vector2 Position
        {
            get { return position; }
        }

        public Lifepack_GUI(LifePack life) 
        {
            lif = life;
            timeLeftToDisappear = (int)life.getTime();
            position = new Vector2(32 * life.getX(), 32 * life.getY());
            t = new Timer();
            t.Interval = 1;
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Lifepack");
        }

        long count = 0;
        bool removed;
        public void Update(GameTime gametime)
        {
           
        }

        public int getXValue()
        {
            return 2;
        }

        public int getYValue()
        {
            return 1;
        }

        public void isCollected()
        {
            //set to true if a player collects lifepacks
            Collected = false;
        }

        int countDraw = 0;
        public void Draw(SpriteBatch spriteBatch, List<Lifepack_GUI> co, ResponseHandler rh)
        {
            countDraw++;
            int timeLeftToDisappearinSec = (int)timeLeftToDisappear / 1000;
            if (countDraw == timeLeftToDisappearinSec * 10)
            {
                removed = true;
                List<LifePack> c = rh.getDataStorage().getLifePacks();
                var Coi = c.Find(d => d.getX() == (position.X / size) && d.getY() == (position.Y / size));
                Coi.setDisappeared();
                //co.Remove(this);

            }
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (removed == false) spriteBatch.Draw(texture, rectangle, Color.White);

        }
    }
}
