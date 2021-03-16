using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Servertest
{
    class _2DMp : WebSocketBehavior
    {
        public List<Player> players = new List<Player>();
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            //Console.WriteLine("Got Message: " + msg);
            if (msg.Contains("Pos:"))
            {
                SetNewPos(msg);
            }
        }

        protected override void OnOpen()
        {
            Console.WriteLine("New Player");
            Player newPlayer = new Player();
            players.Add(newPlayer);
            var msg = "ID: " + newPlayer.id;
            Send(msg);
        }

        protected void SetNewPos(string msg)
        {
            var stringArray = msg.Split(":".ToCharArray());
            stringArray = stringArray[1].Split("/".ToCharArray());
            string x = stringArray[0];
            string y = stringArray[1];
            string id = stringArray[2];
            //Console.WriteLine("After second split: x:" + x + ", y:" + y + " ID: " + id);
            Guid guid = Guid.Parse(id);
            Player target = FindPlayer(guid);
            float xPos = float.Parse(x);
            float yPos = float.Parse(y);
            target.SetPos(xPos, yPos);
        }

        protected Player FindPlayer(Guid id)
        {
            foreach(Player p in players)
            {
                if(p.id == id)
                {
                    return p;
                }
            }
            Console.WriteLine("The player with the id: " + id + " does not exist but the programm is still trying to find him.");
            return players[0];
        }
    }

    public class Program
    {
        /*public static void Main(string[] args)
        {
            var wssv = new WebSocketServer(9000);
            wssv.AddWebSocketService<_2DMp>("/2dmp");
            wssv.Start();
            Console.ReadKey(true);
            wssv.Stop();
        }*/
    }

    public class Player
    {
        public Guid id;
        public float[] pos;

        public Player()
        {
            id = Guid.NewGuid();
            pos = new float[] { 0f, 0f};
        }

        public void SetPos(float x, float y)
        {
            pos = new float[] { x, y };
            Console.WriteLine(id + "s new Pos: " + pos[0] + "/" + pos[1]);
        }
    }
}
