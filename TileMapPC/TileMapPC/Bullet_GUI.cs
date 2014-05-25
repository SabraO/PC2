using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileMapPC
{
    public class Bullet_GUI
    {
        private Texture2D textureUp;
        private Texture2D textureDown;
        private Texture2D textureLeft;
        private Texture2D textureRight;

        private Vector2 position;
        private Rectangle rectangle;

        private bool shot;
        private bool hit;
        private int currentDirection;
        private Vector2 finalPosition;
        public Map_GUI map;
        public Vector2 Position
        {
            get { return position; }
        }

        public Bullet_GUI()
        {
        }

        public void Load(ContentManager Content)
        {
            textureUp = Content.Load<Texture2D>("Bullet_Up");
            textureDown = Content.Load<Texture2D>("Bullet_Down");
            textureLeft = Content.Load<Texture2D>("Bullet_Left");
            textureRight = Content.Load<Texture2D>("Bullet_Right");
        }

        int count = 0;
        public void Update(GameTime gametime,Map_GUI map)
        {
            if (count != 0)
            {
                setPosition(position);
                isHit(position,map);
            }
            rectangle = new Rectangle((int)position.X, (int)position.Y, textureUp.Width, textureUp.Height);//since all images are same size
            count++;
            
        }

        public Vector2 getFinalPosition()
        {
            return finalPosition;
        }

        public void isHit(Vector2 newPosition,Map_GUI map)
        {
            //if newPosition or positions on the way contains a player

            int x = (int)newPosition.X / 32;
            int y = (int)newPosition.Y / 32;
            //Brick = 1, Stone = 2
            if (x > 2 && x < 7 && y > 2 && y < 7){

                #region
                if (currentDirection == 0)//change y,x const
                {
                    #region
                    for (int i = y + 3; i >= y; i--)
                    {
                        int num = map.Contents[i, x];
                        if (num == 1 || num == 2)
                        {
                            if (num == 2) hit = true;
                            else
                            {
                                var brick = map.BrickTiles.First(d => d.Rectangle.X == (x * map.Size) && d.Rectangle.Y == (i * map.Size));
                                int damage = brick.getDamageLevel();
                                if (damage != 4) hit = true;
                            }
                            break;
                        }
                    }
                    #endregion
                }
                else if (currentDirection == 2)//change y,x const
                {
                    #region
                    for (int i = y - 3; i <= y; i++)
                    {
                        int num = map.Contents[i, x];
                        if (num == 1 || num == 2)
                        {
                            if (num == 2) hit = true;
                            else
                            {
                                var brick = map.BrickTiles.First(d => d.Rectangle.X == (x * map.Size) && d.Rectangle.Y == (i * map.Size));
                                int damage = brick.getDamageLevel();
                                if (damage != 4) hit = true;
                            }
                            break;
                        }
                    }
                    #endregion
                }
                else if (currentDirection == 1)//change x,y const
                {
                    #region
                    for (int i = x - 3; i <= x; i++)
                    {
                        int num = map.Contents[y, i];
                        if (num == 1 || num == 2)
                        {
                            if (num == 2) hit = true;
                            else 
                            {
                                var brick = map.BrickTiles.First(d => d.Rectangle.X == (i*map.Size) && d.Rectangle.Y == (y*map.Size));
                                int damage = brick.getDamageLevel();
                                if (damage != 4) hit = true;
                            }
                            break;
                        }
                    }
                    #endregion   
                }
                else if (currentDirection == 3)//change x,y const
                {
                    #region
                    for (int i = x + 3; i > x; i--)
                    {
                        int num = map.Contents[y, i];
                        if (num == 1 || num == 2)
                        {
                            if (num == 2) hit = true;
                            else
                            {
                                var brick = map.BrickTiles.First(d => d.Rectangle.X == (i * map.Size) && d.Rectangle.Y == (y * map.Size));
                                int damage = brick.getDamageLevel();
                                if (damage != 4) hit = true;
                            }
                            break;
                        }
                    }
                    #endregion
                }
            }
                #endregion

        }

        public void isShot(Vector2 newPosition, int newDirection,Map_GUI map)
        {
            //Console.WriteLine("is in isshot");
            shot = true;//setting inital position
            currentDirection = newDirection;
            position = newPosition;
            //setPosition(newPosition);
            this.map = map;
            //isHit(position,map);
            setFinalPosition();
        }

        public void setFinalPosition()
        {
            int x = (int)position.X / 32;
            int y = (int)position.Y / 32;

            if (currentDirection == 0)//change y,x const
            {
                #region
                int i;
                for (i = y; i >= 0; i--)
                {
                    int num = map.Contents[i, x];
                    if (num == 1 || num == 2)
                    {
                        if (num == 2) hit = true;
                        else
                        {
                            var brick = map.BrickTiles.First(d => d.Rectangle.X == (x * map.Size) && d.Rectangle.Y == (i * map.Size));
                            int damage = brick.getDamageLevel();
                            if (damage != 4) hit = true;
                        }
                        break;
                    }
                }
                finalPosition = new Vector2(32 * x, 32 * i);
                #endregion
            }
            else if (currentDirection == 2)//change y,x const
            {
                #region
                int i;
                for (i = y; i <= 20 - 1; i++)
                {
                    int num = map.Contents[i, x];
                    if (num == 1 || num == 2)
                    {
                        if (num == 2) hit = true;
                        else
                        {
                            var brick = map.BrickTiles.First(d => d.Rectangle.X == (x * map.Size) && d.Rectangle.Y == (i * map.Size));
                            int damage = brick.getDamageLevel();
                            if (damage != 4) hit = true;
                        }
                        break;
                    }
                }
                finalPosition = new Vector2(32 * x, 32 * i);
                #endregion
            }
            else if (currentDirection == 1)//change x,y const
            {
                #region
                int i;
                for (i = x; i <= 20 - 1; i++)
                {
                    int num = map.Contents[y, i];
                    if (num == 1 || num == 2)
                    {
                        if (num == 2) hit = true;
                        else
                        {
                            var brick = map.BrickTiles.First(d => d.Rectangle.X == (i * map.Size) && d.Rectangle.Y == (y * map.Size));
                            int damage = brick.getDamageLevel();
                            if (damage != 4) hit = true;
                        }
                        break;
                    }
                }
                finalPosition = new Vector2(32 * i, 32 * y);
                #endregion
            }
            else if (currentDirection == 3)//change x,y const
            {
                #region
                int i;
                for (i = x; i >= 0; i--)
                {
                    int num = map.Contents[y, i];
                    if (num == 1 || num == 2)
                    {
                        if (num == 2) hit = true;
                        else
                        {
                            var brick = map.BrickTiles.First(d => d.Rectangle.X == (i * map.Size) && d.Rectangle.Y == (y * map.Size));
                            int damage = brick.getDamageLevel();
                            if (damage != 4) hit = true;
                        }
                        break;
                    }
                }
                finalPosition = new Vector2(32 * x, 32 * i);
                #endregion
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            int x = (int)newPosition.X / 32;
            int y = (int)newPosition.Y / 32;
            if (x >2 && y >2 && y < 7 && x < 7)//check if going to hit the screen
            {
                if (currentDirection == 0)//Up
                {
                    position = new Vector2((32 * x) + 15, (32 * (y - 3)));
                }
                else if (currentDirection == 1)//Right
                {
                    position = new Vector2((32 * (x + 3)) + 35, (32 * y) + 15);
                }
                else if (currentDirection == 2)//Down
                {
                    position = new Vector2((32 * x) + 15, (32 * (y + 3)) + 35);
                }
                else if (currentDirection == 3)//Left
                {
                    position = new Vector2((32 * (x - 3)), (32 * y) + 15);
                }
            }
            else
            {
                hit = true;
            }
        }

        public void setDirection(int newDirection)
        {
            currentDirection = newDirection;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (shot == true && hit == false) 
            DrawBullet(spriteBatch);
        }

        public void DrawBullet(SpriteBatch spriteBatch)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, textureUp.Width, textureUp.Height);//since all images are same size
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
        }
    }
}
