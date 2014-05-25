using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapPC
{
    public class Decision
    {
        int[,] array = new int[20, 20];
        public void initialize(Brick[] bricks,Cell[] stones,Cell[] water) {
            
            //heuristic values for bricks
            foreach (Brick b in bricks) {
                array[b.getX(), b.getY()] = -25000;
            }
            //heuristic values for stones
            foreach (Cell s in stones)
            {
                array[s.getX(), s.getY()] = -25000;
            }
            //heuristic values for water
            foreach (Cell s in water)
            {
                array[s.getX(), s.getY()] = -50000;
            }
        }
        public void update(Player[] players,List<CoinPile> coinpiles,List<LifePack> lifepacks,int myPlayer) {
            //heuristic values for players
            foreach (Player p in players)
            {
                if(p.getHealth()>0){
                    if (p.getID() == myPlayer)//mark other players except me as opponents
                    {
                        array[p.getX(), p.getY()] = 10;
                    }
                    else {
                        array[p.getX(), p.getY()] = -10000;
                    }
                }
                
            }
             //heuristic values for lifepacks
            foreach (LifePack b in lifepacks)
            {
                if(b !=null){
                    if (b.disappeared == false || b.isCollected == false)
                    {
                        array[b.getX(), b.getY()] = 50;
                    }
                }     
                
            }
          
            //heuristic values for coinpiles
                foreach (CoinPile b in coinpiles)
                {
                    if(b!=null){

                        if(b.disappeared==false|| b.isCollected==false)    array[b.getX(), b.getY()] = 100;
                    }
                    
                }
            
        }
        public int takeDecision(DataStorage ds) {
            //consider grid as blank and add heuristic value
            for (int m = 0; m < ds.size; ++m) {
                for (int n = 0; n < ds.size; ++n) {
                    array[m, n] = 10;
                }
            }
            //initalize for bricks,stones and water
            initialize(ds.getBricks(), ds.getStones(), ds.getWater());
            //get my position
            int p = ds.getMyPlayer();
            //update heuristic values for players,coinpiles and lifepacks
            update(ds.getPlayers(), ds.getCoinPiles(), ds.getLifePacks(), ds.getMyPlayer());

            int i = 0;int j = 0;//position of my player
            int dir = 0;//direction of my player
            Player[] players = ds.getPlayers();
            for (int k = 0; k < players.Length; ++k) {
                if (players[k].getID() == p) {
                    i = players[k].getX();
                    j = players[k].getY();
                    dir = players[k].getDirection();
                }
            }

            //consider immediate neighbours
            int[] neighbours = new int[4];
            for (int z = 0; z < 4; z++)
            {
                neighbours[z] = 10;
            }
            //find heuristic values for neighbors
            if (j > 0)
            {
                neighbours[0] = array[i,j - 1];//up
            }
            if (i < ds.size - 1)
            {
                neighbours[1] = array[i + 1,j];//right
            }
            if (j < ds.size - 1)
            {
                neighbours[2] = array[i,j + 1];//down
            }
            if (i > 0)
            {
                neighbours[3] = array[i - 1,j];//left
            }
            if (i == 0) {
                neighbours[3] = 0;
            }
            if (j == 0)
            {
                neighbours[0] = 0;
            }
            if (i == ds.size-1)
            {
                neighbours[1] = 0;
            }
            if (j == ds.size - 1)
            {
                neighbours[2] = 0;
            }
            //check neighborhood and take decision
            int decision = 1;
            int value = neighbours[0];
 
            for (int k = 1; k < 4; ++k)
            {
                    if (neighbours[k] == -10000 && dir==k)//check whther an attack is possible
                    {
                        value = -10000;
                        decision = 5;
                        break;
                    }
                    if (neighbours[k] > value)
                    {
                        decision = k + 1;
                        value = neighbours[k];
                    }
            }
            
            //if (value <=0) {//has obstacles around
                
                
            
            return decision;
        }
        //public int neighborSum() { 
        
        
       /* //Minmax tree to take decision
        public int MinMax (DataStorage ds) {
          return MaxMove (ds);
        }
 
        public int MaxMove (DataStorage ds) {
          if (GameEnded(game) || DepthLimitReached()) {
            return EvalGameState(game, MAX);
          }
          else {
            best_move < - {};
            moves <- GenerateMoves(game);
            ForEach moves {
               move <- MinMove(ApplyMove(game));
               if (Value(move) > Value(best_move)) {
                  best_move < - move;
               }
            }
            return best_move;
          }
        }
 
        MinMove (GamePosition game) {
          if (GameEnded(game) || DepthLimitReached()) {
            return EvalGameState(game, MIN);
          }
          else {
            best_move <- {};
            moves <- GenerateMoves(game);
            ForEach moves {
               move <- MaxMove(ApplyMove(game));
               if (Value(move) > Value(best_move)) {
                  best_move < - move;
               }
            }
            return best_move;
          }
        }*/
    }
}
