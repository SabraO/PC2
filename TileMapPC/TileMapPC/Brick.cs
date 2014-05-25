using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapPC
{
    public class Brick:Cell
    {
        private int damage;
        public void setDamage(int n){
            damage=n;
        }
        public int getDamage() {
            return damage;
        }
        public void view()
        {
            Console.Write(getX());
            Console.Write(" ");
            Console.Write(getY());
            Console.Write(" ");
            Console.Write(getDamage());
            Console.Write(" ");
          
            Console.WriteLine(" ");

        }
    }
}
