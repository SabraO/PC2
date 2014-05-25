using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace TileMapPC
{
    public class Communicator 
    {
        TcpListener listener;

        /*public static void Main(String[] args)
        {
            Communicator com = new Communicator();
            com.writeToServer("JOIN#");
            com.readFromServer();
            com.readFromServer();
            com.readFromServer();
            com.readFromServer();
            com.readFromServer();
            com.readFromServer();
            com.readFromServer();
            Thread.Sleep(7000);
            com.writeToServer("UP#");
        }*/

        public Communicator()
        {
            listener = new TcpListener(IPAddress.Any, 7000);
            listener.Start();
        }

        public void writeToServer(String str)
        {
            TcpClient tcpclnt = new TcpClient();
            tcpclnt.Connect("127.0.0.1", 6000);
            Stream stm = tcpclnt.GetStream();
            StreamWriter sw = new StreamWriter(stm);

            sw.Write(str);
            sw.Flush();
            tcpclnt.Close();
        }

        public String readFromServer()
        {
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream); ;
            String message = reader.ReadLine();
            reader.Close();
            stream.Close();
            return message;
        }
      
        
        
    }  
}
