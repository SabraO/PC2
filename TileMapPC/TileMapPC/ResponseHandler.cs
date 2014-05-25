using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapPC
{
    public class ResponseHandler
    {
        DataStorage ds = new DataStorage(20);
        public void handle(String s)
        {
            if (s != null)
            {
                if (this.hasError(s))
                {           
                }
                else if (s.StartsWith("I"))
                {
                    String str = s.Substring(3, 1);
                    ds.setMyPlayer(Convert.ToInt16(str));
                    Console.WriteLine("Initializing the client");
                    //Console.WriteLine(s.Substring(0, s.Length - 1));
                    setGrid(s.Substring(0, s.Length - 1));
                    ds.viewArea();
                }
                else if (s.StartsWith("S"))
                {
                    ds.setPlayers(s.Substring(0, s.Length - 1));
                    Console.WriteLine("Initializing the players");
                    viewPlayers();
                  
                }
                else if (s.StartsWith("L"))
                {
                    Console.WriteLine("updating the LifePacks");
                    ds.setLifePacks(s.Substring(0, s.Length - 1));
                    viewLifePacks();
                }
                else if (s.StartsWith("C"))
                {
                    Console.WriteLine("updating the CoinPiles");
                    ds.setCoinPiles(s.Substring(0, s.Length - 1));
                    viewCoinPiles();
                }
               
                else if (s.StartsWith("G"))
                {
                    ds.setPlayers(s.Substring(0, s.Length - 1));
                    ds.coinCollected();
                    ds.lifeCollected();
                    Console.WriteLine("updating the players");
                    viewPlayers();
                    viewBricks();
                }
               
            }
        }
        public DataStorage getDataStorage() {
            return ds;
        }
        public Boolean hasError(String s)
        {
            if (s.Equals("OBSTACLE#"))
            {
                return true;
            }
            else if (s.Equals("CELL_OCCUPIED#"))
            {
                return true;
            }
            else if (s.Equals("DEAD#"))
            {
                return true;
            }
            else if (s.Equals("TOO_QUICK#"))
            {
                return true;
            }
            else if (s.Equals("INVALID_CELL#"))
            {
                return true;
            }
            else if (s.Equals("GAME_HAS_FINISHED#"))
            {
                // DataStore.isFinished = true;
                return true;
            }
            else if (s.Equals("GAME_NOT_STARTED_YET#"))
            {
                return true;
            }
            else if (s.Equals("NOT_A_VALID_CONTESTANT#"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void viewPlayers(){
            Player[] players = ds.getPlayers();
            foreach (Player ply in players) {
                ply.display();
            }

        }
        public void viewBricks() {
            Console.WriteLine("Updating brick damage");
            Brick[] bricks = ds.getBricks();
            foreach (Brick brick in bricks) {
                brick.view();
            }
        }
        public void setGrid(String s) {
            String[] details = s.Split(':');
            //Console.WriteLine(s.Substring(2, s.Length - 2));
            ds.updateArea(s.Substring(2,s.Length-2));
            if (details[0] == "I")
            {
                for (int i = 2; i < 5; ++i)
                {
                    String[] facts = details[i].Split(';');
                    if(i==2){
                        ds.setBricks(facts);
                    }
                    if (i == 3)
                    {
                        ds.setStones(facts);
                    }
                    if (i == 4)
                    {
                        ds.setWater(facts);
                    }
                    
                }
            }
        }
        public void viewCoinPiles() {
            List<CoinPile> coinpiles = ds.getCoinPiles();
            for(int i=0;i<coinpiles.Count;++i){
                if (coinpiles[i] != null) {
                    coinpiles[i].view();
                }
            }
        }
        public void viewLifePacks() {
            List<LifePack> lifepacks = ds.getLifePacks();
            for (int i = 0; i < lifepacks.Count; ++i) {
                if (lifepacks[i] != null) {
                    lifepacks[i].view();
                }
            }
        }
       
    }
}
