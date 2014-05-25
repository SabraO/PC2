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
    public class Coin_GUI
    {
        private Texture2D texture;
        
        private Vector2 position;
        private Rectangle rectangle;

        private int value;
        private long timeLeftToDisappear;//in seconds
        private bool Collected;
        CoinPile coi;
        Timer t;
        public bool disappeared=false;
        int size = 32;

        public Vector2 Position
        {
            get { return position; }
        }

        public Coin_GUI(CoinPile pile)
        {
            coi = pile;
            value = pile.getValue();
            timeLeftToDisappear = (int)pile.getTime();
            position = new Vector2(32 * pile.getX(), 32 * pile.getY());
            t = new Timer();
            t.Interval = 1;
            
            
        }
        private void t_Tick(object sender,EventArgs e) {
            count++;
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Coin");            
        }

        int count = 0;
        public bool removed;
        public void Update(GameTime gametime)
        {
        }

        public void isCollected()
        {
            //set to true if a player collects coins
            Collected = false;
        }

        public void setRemoved()
        {
            removed = true;
        }

        int countDraw=0;
        public void Draw(SpriteBatch spriteBatch,List<Coin_GUI> co,ResponseHandler rh)
        {
            countDraw++;
            int timeLeftToDisappearinSec = (int)timeLeftToDisappear / 1000;
            if (countDraw == timeLeftToDisappearinSec*20 )
            {
                removed = true;
                List<CoinPile> c = rh.getDataStorage().getCoinPiles();
                var Coi = c.Find(d => d.getX() == (position.X/size) && d.getY() == (position.Y/size));
                Coi.setDisappeared();
                //co.Remove(this);

            }
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if(removed == false) spriteBatch.Draw(texture, rectangle, Color.White);
            
        }
    }
}
