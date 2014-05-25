using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace TileMapPC
{
    class Game
    {
        private Timer _creatorTimer;
        Communicator com = new Communicator();
        ResponseHandler handler = new ResponseHandler();
        Boolean notWriting=true;
        Decision decision=new Decision();
        Game game;
        

        // BackgroundWorker's work
        private void CreatorWork(object sender, EventArgs e)
        {
            if (sender is Game) {
                this.game = (Game)sender;
            }
            // note: there's only a start timeout, and no repeat timeout
            //   so this will fire only once
            _creatorTimer = new Timer(CreatorLoop, null, 5, Timeout.Infinite);

            // some other code that worker is doing while the timer is active
           
            String s = com.readFromServer();

            do
            {
                Console.WriteLine("reading");
                handler.handle(s);
                s = com.readFromServer();
            } while (notWriting);
            
        }

        private void CreatorLoop(object state)
        {
            Console.WriteLine("In CreatorLoop...");
            //notWriting = false;
            String s = game.write();
            if (s!=null) {
                com.writeToServer(s);
                string str=com.readFromServer();
                handler.handle(str);
            }
            //notWriting = true;
           // Thread.Sleep(1000);

            // Reenable timer
            Console.WriteLine("Exiting...");

            // now we reset the timer's start time, so it'll fire again
            //   there's no chance of reentrancy, except for actually
            //   exiting the method (and there's no danger even if that
            //   happens because it's safe at this point).
            _creatorTimer.Change(5, Timeout.Infinite);
        }
        public void join() {
            com.writeToServer("JOIN#");
            String s = com.readFromServer();
            if (s.Contains("I")) {
                handler.handle(s);
              
            }
            s = com.readFromServer();
            if (s.Contains("S"))
            {
                handler.handle(s);

            }
            //initilizing the decision
            Brick[] b=handler.getDataStorage().getBricks();
            Cell[] st=handler.getDataStorage().getStones();
            Cell[] w=handler.getDataStorage().getWater();
            decision.initialize(b,st,w);
        }
        /*public static void Main(String[] args)
        {
            Game game = new Game();
            game.join();
            EventArgs e=new EventArgs();
            game.CreatorWork(game,e);
            

        }*/
        
        public String write(){
            Console.WriteLine("writing to server");
            //Random rnd = new Random();
            //int n = rnd.Next(1, 5);

            //get decision
            int n = decision.takeDecision(handler.getDataStorage());
            Console.WriteLine(n);
            if (n == 1) {
                return "UP#";
            }
            if (n == 2)
            {
                return "RIGHT#";
            }
            if (n == 3)
            {
                return "DOWN#";
            }
            if (n == 4)
            {
                return "LEFT#";
            }
            if (n == 5)
            {
                return "SHOOT#";
            }
            else {
                return null;
            }
        }
   
    }
    
}
