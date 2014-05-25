using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileMapPC
{
    public class Tiles_GUI
    {
        protected Texture2D texture;

        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);

        }
    }

    public class Water : Tiles_GUI
    {
        public Water(Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Water");
            this.Rectangle = newRectangle;
        }

    }

    public class Brick_GUI : Tiles_GUI
    {
        Texture2D texture_75;
        Texture2D texture_50;
        Texture2D texture_25;
        private int damageLevel = 0; //0 - no damage, 1 - 25% damage, 2 - 50% damage, 3 - 75% damage, 4 - 100%damage
        Blank blankTile;

        public Brick_GUI(Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Brick_full");
            texture_75 = Content.Load<Texture2D>("Brick_75");
            texture_50 = Content.Load<Texture2D>("Brick_50");
            texture_25 = Content.Load<Texture2D>("Brick_25");
            this.Rectangle = newRectangle;
            blankTile = new Blank(Rectangle);
        }

        public void setDamageLevel(int damageLevel)
        {
            this.damageLevel = damageLevel;
        }

        public int getDamageLevel()
        {
            return damageLevel;
        }

        public void newDraw(SpriteBatch spriteBatch)
        {
            if (damageLevel == 0)
                spriteBatch.Draw(texture, Rectangle, Color.White);
            else if (damageLevel == 1)
                spriteBatch.Draw(texture_75, Rectangle, Color.White);
            else if (damageLevel == 2)
                spriteBatch.Draw(texture_50, Rectangle, Color.White);
            else if (damageLevel == 3)
                spriteBatch.Draw(texture_25, Rectangle, Color.White);
            else if (damageLevel == 4)
                spriteBatch.Draw(blankTile.getTexture(), Rectangle, Color.White);
        }

    }

    public class Stone : Tiles_GUI
    {
        public Stone(Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Stone");
            this.Rectangle = newRectangle;
        }

    }

    public class Blank : Tiles_GUI
    {
        public Blank(Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Blank");
            this.Rectangle = newRectangle;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

    }

}
