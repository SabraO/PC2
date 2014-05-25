using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapPC
{
    public class Grid
    {
        public String[,] grid;
        int size;
        public void initialize(int size)
        {
            this.size = size;
            grid=new String[size,size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    grid[i,j] = "N";
                }
            }
        }
        public void view() {
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    Console.Write(grid[i,j]);
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
        public void update(String plan) {
            String[] details = plan.Split(':');

            //setting brick positions

            String[] brick=details[1].Split(';');
            for (int k = 0; k < brick.Length; ++k) {
                String[] b = brick[k].Split(',');
                int i = Convert.ToInt16(b[0]);
                int j = Convert.ToInt16(b[1]);
                grid[j, i] = "B";

            }

            //setting stone positions

            String[] stone = details[2].Split(';');
            for (int k = 0; k < stone.Length; ++k)
            {
                String[] s = stone[k].Split(',');
                int i = Convert.ToInt16(s[0]);
                int j = Convert.ToInt16(s[1]);
                grid[j, i] = "S";

            }
            //setting water positions

            String[] water = details[3].Split(';');
            for (int k = 0; k < water.Length; ++k)
            {
                String[] w = water[k].Split(',');
                int i = Convert.ToInt16(w[0]);
                int j = Convert.ToInt16(w[1]);
                grid[j, i] = "W";

            }
        }

    }
}
