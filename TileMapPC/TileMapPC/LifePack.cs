using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapPC
{
    public class LifePack : Cell
    {
        private int time;
        public bool started;
        public bool disappeared;
        public bool isCollected;
        public int getTime()
        {
            return time;
        }
        public void setTime(int n)
        {
            time = n;
        }
        public void view() {
            Console.Write(getX());
            Console.Write(" ");
            Console.Write(getY());
            Console.Write(" ");
            Console.Write(getTime());
            Console.Write(" ");
            Console.WriteLine(" ");
        }

        public void setDisappeared()
        {
            disappeared = true;
        }
    }
}
