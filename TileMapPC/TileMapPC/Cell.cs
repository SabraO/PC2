using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapPC
{
    public class Cell
    {
        private int X;
        private int Y;
        public void setX(int x) {
            X = x;
        }
        public int getX()
        {
            return X;
        }
        public void setY(int y)
        {
            Y = y;
        }
        public int getY()
        {
            return Y;
        }
    }
}
