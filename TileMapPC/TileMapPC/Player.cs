using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapPC
{
    public class Player : Cell
    {
        private int id=0;
        private int direction=0;
        private int shot=0;
        private int health=0;
        private int coins=0;
        private int points=0;
       

 
         public Player(String[] facts){
             if(facts.Length==3){
                 //set player id
                String str=facts[0].Substring(1,1);
                id=Convert.ToInt16(str);

                 //set player position
                 String[] temp=facts[1].Split(',');
                 if(temp.Length==2){
                    setX(Convert.ToInt16(temp[0]));
                    setY(Convert.ToInt16(temp[1]));
                 }

                 //set player direction
                 direction=Convert.ToInt16(facts[2]);
             }
         }
         public int getID() {
             return id;
         }
         public int getDirection() {
             return direction;
         }
         public int getHealth() {
             return health;
         }
         public int getCoins()
         {
             return coins;
         }
         public int getPoints()
         {
             return points;
         }
        
        
        public void display(){
            Console.Write(this.id);
            Console.Write(" ");
            Console.Write(getX());
            Console.Write(",");
            Console.Write(getY());
            Console.Write(" ");
            Console.Write(this.direction);
            Console.Write(" ");
            Console.Write(this.shot);
            Console.Write(" ");
            Console.Write(this.health);
            Console.Write(" ");
            Console.Write(this.coins);
            Console.Write(" ");
            Console.Write(this.points);
            Console.WriteLine(" ");
        }
        public void update(String[] facts){
            if (facts.Length == 7) { 
                //set player position
                String[] temp = facts[1].Split(',');
                if (temp.Length == 2)
                {
                    setX(Convert.ToInt16(temp[0]));
                    setY(Convert.ToInt16(temp[1]));
                }

                //set player direction
                direction = Convert.ToInt16(facts[2]);

                //set whether shot
                shot = Convert.ToInt16(facts[3]);

                //set health
                health = Convert.ToInt16(facts[4]);

                //set coins
                coins = Convert.ToInt16(facts[5]);

                //set points
                points = Convert.ToInt16(facts[6]);
            }
        }
        }
    
}
