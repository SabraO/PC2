using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapPC
{
    public class DataStorage
    {
        public Grid area;
        public int myPlayer;
        public Player[] players=new Player[5];
        public List<CoinPile> coinpiles = new List<CoinPile>();
        public List<LifePack> lifepacks = new List<LifePack>();
        public Cell[] stones;
        public Cell[] watercells;
        public Brick[] bricks;
        public int size;

        public DataStorage(int size)
        {
            area = new Grid();
            area.initialize(size);
            this.size = size;
        }
        public void setMyPlayer(int n)
        {
            myPlayer = n;
        }
        public int getMyPlayer()
        {
            return myPlayer;
        }
        public void coinCollected() {
            foreach (Player p in players) {
                int dir = p.getDirection();
                if (dir == 0) {
                    var coin = coinpiles.Find(d => d.getY() == p.getY() - 1);
                    if(coin!=null) coin.isCollected = true;
                }
                if (dir == 1)
                {
                    var coin = coinpiles.Find(d => d.getX() == p.getX() + 1);
                    if (coin != null) coin.isCollected = true;
                }
                if (dir == 2)
                {
                    var coin = coinpiles.Find(d => d.getY() == p.getY() + 1);
                    if (coin != null) coin.isCollected = true;
                }
                if (dir == 3)
                {
                    var coin = coinpiles.Find(d => d.getX() == p.getX() - 1);
                    if (coin != null) coin.isCollected = true;
                }
            }

        }

        public void lifeCollected()
        {
            foreach (Player p in players)
            {
                int dir = p.getDirection();
                if (dir == 0)
                {
                    var life = lifepacks.Find(d => d.getY() == p.getY() - 1);
                    if (life != null) life.isCollected = true;
                }
                if (dir == 1)
                {
                    var life = lifepacks.Find(d => d.getX() == p.getX() + 1);
                    if (life != null) life.isCollected = true;
                }
                if (dir == 2)
                {
                    var life = lifepacks.Find(d => d.getY() == p.getY() + 1);
                    if (life != null) life.isCollected = true;
                }
                if (dir == 3)
                {
                    var life = lifepacks.Find(d => d.getX() == p.getX() - 1);
                    if (life != null) life.isCollected = true;
                }
            }

        }
        public void initializeDataStorage(String str)
        {
            area.view();
            String plan = str.Substring(2, str.Length - 3);
            area.update(plan);
            area.view();

        }
        public void updateArea(String s) {
            area.update(s);
        }
        public void viewArea() {
            area.view();
        }

        public List<LifePack> getLifePacks() {
            return lifepacks;
        }
        public List<CoinPile> getCoinPiles() {
            return coinpiles;
        }
        public Brick[] getBricks() {
            return bricks;
        }
        public Cell[] getStones() {
            return stones;
        }
        public Cell[] getWater() {
            return watercells;
        }
        public void setCoinPiles(String s) {

            String[] details = s.Split(':');
            if (details[0] == "C")
            {
                    //set coin pile
                CoinPile coin = new CoinPile();
                    
                   
                    //set position
                    String[] b = details[1].Split(',');
                    int i = Convert.ToInt16(b[0]);
                    int j = Convert.ToInt16(b[1]);
                    coin.setX(i);
                    coin.setY(j);

                    //set time
                    int time=Convert.ToInt32(details[2]);
                    coin.setTime(time);

                    //set value
                    int value = Convert.ToInt32(details[3]);
                    coin.setValue(value);
                    coin.started = true;
                    coinpiles.Add(coin);
                }
            
        }
        public void setLifePacks(String s)
        {

            String[] details = s.Split(':');
            if (details[0] == "L")
            {
                //set lifePack
                LifePack lifepack = new LifePack();

                //set position
                String[] b = details[1].Split(',');
                int i = Convert.ToInt32(b[0]);
                int j = Convert.ToInt32(b[1]);
                lifepack.setX(i);
                lifepack.setY(j);

                //set time
                int time = Convert.ToInt32(details[2]);
                lifepack.setTime(time);
                lifepack.started = true;
                lifepacks.Add(lifepack);

            }

        }
        public void setBricks(String[] str)
        {
            bricks = new Brick[str.Length];
            for (int k = 0; k < str.Length; ++k)
            {
                bricks[k] = new Brick();
                String[] b = str[k].Split(',');
                int i = Convert.ToInt16(b[0]);
                int j = Convert.ToInt16(b[1]);
                bricks[k].setX(i);
                bricks[k].setY(j);
            }
        }

        public void setStones(String[] str)
        {
            stones = new Cell[str.Length];
            for (int k = 0; k < str.Length; ++k)
            {
                stones[k] = new Cell();
                String[] b = str[k].Split(',');
                int i = Convert.ToInt16(b[0]);
                int j = Convert.ToInt16(b[1]);
                stones[k].setX(i);
                stones[k].setY(j);
            }
        }

        public void setWater(String[] str)
        {
            watercells = new Cell[str.Length];
            for (int k = 0; k < str.Length; ++k)
            {
                watercells[k] = new Cell();
                String[] b = str[k].Split(',');
                int i = Convert.ToInt16(b[0]);
                int j = Convert.ToInt16(b[1]);
                watercells[k].setX(i);
                watercells[k].setY(j);
            }
        }
        public Player[] getPlayers() {
            return players;
        }
        public void updateBricks(String str) {
            String[] facts =str.Split(';');
            if (facts.Length == bricks.Length) {
                
                for (int i = 0; i < facts.Length; ++i) {
                    String[] s = facts[i].Split(',');
                    int x = Convert.ToInt16(s[0]);
                    int y = Convert.ToInt16(s[1]);
                    if (x == bricks[i].getX() && y == bricks[i].getY()) {
                        bricks[i].setDamage(Convert.ToInt16(s[2]));
                    }
                }
            }
        }
        public void setPlayers(String s)
        {
            String[] details = s.Split(':');
            if (details[0] == "S")
            {    
                for (int i = 1; i < 6; ++i)
                {
                    String[] facts = details[i].Split(';');
                    players[i - 1] = new Player(facts);
                }
            }
            if (details[0] == "G")
            {
                if (players != null)
                {
                    for (int i = 1; i < 6; ++i)
                    {
                        String[] facts = details[i].Split(';');
                        String str = facts[0].Substring(1, 1);
                        if (Convert.ToInt16(str) == i - 1)
                        {
                            players[i - 1].update(facts);
                        }
                    }
                }
                updateBricks(details[6]);

            }
        }
    }

    
}
