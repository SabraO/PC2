using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TileMapPC
{
    public class CoinPile:Cell
    {
        private int time;
        private int value;
        public bool started;
        public bool disappeared;
        public bool isCollected;
        public int getTime(){
            return time;
        }
        public int getValue(){
            return value;
        }
        public void setTime(int n){
            time=n;
        }
        public void setValue(int n){
            value = n;
        }
        public void view()
        {
            Console.Write(getX());
            Console.Write(" ");
            Console.Write(getY());
            Console.Write(" ");
            Console.Write(getTime());
            Console.Write(" ");
            Console.Write(getValue());
            Console.Write(" ");
            Console.WriteLine(" ");

        }

        public void setDisappeared()
        {
            disappeared = true;
        }
    }
}
