using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileMapPC
{
    public class Player_GUI
    {
        private Texture2D textureUp;
        private Texture2D textureRight;
        private Texture2D textureLeft;
        private Texture2D textureDown;
        
        private Vector2 position;
        private Rectangle rectangle;
        private int currentDirection;
        private int x;
        private int y;
        private int health;
        private int playerNo;
        private string serverStringReply = "";
        private bool issuedShoot = true;
        
        public Vector2 Position
        {
            get { return position; }
        }

        public Player_GUI(int columnNo, int rowNo, int initialDirection,int Id)
        {
            setPosition(columnNo, rowNo);
            setDirection(initialDirection);
            playerNo = Id;
        }

        public void setPosition(int columnNo, int rowNo)
        {
            position = new Vector2(32 * columnNo, 32 * rowNo);
        }

        public void setDirection(int direction)
        {
            currentDirection = direction;
        }

        public void Load(ContentManager Content)
        {
            textureUp = Content.Load<Texture2D>("player" + playerNo + "_Up");
            textureRight = Content.Load<Texture2D>("player" + playerNo + "_Right");
            textureDown = Content.Load<Texture2D>("player" + playerNo + "_Down");
            textureLeft = Content.Load<Texture2D>("player" + playerNo + "_Left");
        }

        int count = 0;
        #region
        public void Update(GameTime gametime,Map_GUI map)
        {
            /*if (issuedShoot == true)//check if client issued SHOOT#
            {
                bullet.isShot(position, currentDirection,map);
                issuedShoot = false;
            }
            bullet.Update(gametime,map);*/
            
            if (count % 60 == 0)
            {
                if (count != 0)
                {
                    currentDirection = getDirection();
                    position.Y = 32 * getYValue();
                    position.X = 32 * getXValue();
                }
            }

            /* Tank is not rendered if either dead or not a valid contestant,else should be rendered for
             *any other serverStringReply*/
             
            if (getServerStringReply() == "DEAD#" || getServerStringReply() == "NOT_A_VALID_CONTESTANT#") { }
            else
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, textureUp.Width, textureUp.Height);//since all images are same size
            }
            rectangle = new Rectangle((int)position.X, (int)position.Y, textureUp.Width, textureUp.Height);//since all images are same size
            //count++;
        }
        #endregion

        public String getServerStringReply()
        {
            return serverStringReply;
        }

        public void setServerReply()
        {
            /*check if it is a string starting with G -> set isCoordinates = true
              if it is any other OBSTACLE#, DEAD# etc -> setServerStringReply */
        }

        public void setValues(int x, int y, int direction,int health)
        {
            setPosition(x, y);
            setDirection(direction);
            this.health = health;
            //this.x = x;
            //this.y = y;
            //currentDirection = direction;
        }

        public int getXValue()
        {
            return x;
        }

        public int getYValue()
        {
            return y;
        }

        public int getDirection()
        {
            return currentDirection;
        }

        public int getPlayerNo()
        {
            return playerNo;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, textureUp.Width, textureUp.Height);
            if (health != 0)
            {
                #region
                if (currentDirection == 0)
                {
                    spriteBatch.Draw(textureUp, rectangle, Color.White);
                }
                else if (currentDirection == 1)
                {
                    spriteBatch.Draw(textureRight, rectangle, Color.White);
                }
                else if (currentDirection == 2)
                {
                    spriteBatch.Draw(textureDown, rectangle, Color.White);
                }
                else if (currentDirection == 3)
                {
                    spriteBatch.Draw(textureLeft, rectangle, Color.White);
                }
                #endregion
            }
            //bullet.Draw(spriteBatch);
        }
    }
}
