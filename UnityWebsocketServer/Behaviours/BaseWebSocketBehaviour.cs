using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebsocketServer.Models;
using WebSocketSharp.Server;

namespace WebsocketServer
{
    class BaseWebSocketBehaviour : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Player newPlayer = new Player();
            PlayerList.Instance().AddEntry(newPlayer);
            var msg = "ID: " + newPlayer.guid;
            Console.WriteLine("New connection. Returned ID: " + newPlayer.guid);
            Send(msg);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            //Console.WriteLine("Got Message: " + msg);
            if (msg.Contains("Pos:"))
            {
                SetNewPos(msg);
            }
            else if (msg.Contains("Get guid"))
            {
                Player newPlayer = new Player();
                PlayerList.Instance().AddEntry(newPlayer);
                var ret = "ID: " + newPlayer.guid;
                Console.WriteLine("New id for existing connection: " + newPlayer.guid);
                Send(ret);
            }
            else if (msg.Contains("Get:"))
            {
                ReturnData(msg);
            }
            else if (msg.Contains("Delete:"))
            {
                DeletePlayer(msg);
            }
        }

        // Message Format( Pos: Xpos/Ypos/Zpos/id )
        protected virtual void SetNewPos(string msg)
        {
            var stringArray = msg.Split(":".ToCharArray());
            stringArray = stringArray[1].Split("/".ToCharArray());
            string x = stringArray[0];
            string y = stringArray[1];
            string z = stringArray[2];
            string id = stringArray[3];
            //Console.WriteLine("After second split: x:" + x + ", y:" + y + " ID: " + id);
            Guid guid = Guid.Parse(id);
            Player target = PlayerList.Instance().FindEntry(guid);
            float xPos = float.Parse(x);
            float yPos = float.Parse(y);
            float zPos = float.Parse(z);
            target.SetPos(xPos, yPos, zPos);
        }

        protected void ReturnData(string msg)
        {
            Guid guid = ParseGuidDefault(msg);

            Send("Players:" + PlayerList.Instance().GetListAsString(guid));
        }

        protected void DeletePlayer(string msg)
        {
            Guid guid = ParseGuidDefault(msg);
            Console.WriteLine("Player " + guid + " is closing the connection. Good bye.");
            PlayerList.Instance().RemoveEntry(guid);
        }

        protected new void Send(string data)
        {
            byte[] msg = Encoding.UTF8.GetBytes(data);
            Send(msg);
        }

        // Parses a string  to a guid and returns. Only works for a string with this format: Command:Guid
        // The command will be ignored
        protected Guid ParseGuidDefault(string msg)
        {
            var stringArray = msg.Split(":".ToCharArray());
            Guid guid = Guid.Parse(stringArray[1]);
            return guid;
        }

        public virtual void Test()
        {
            Player testPlayer = new Player();
            PlayerList.Instance().AddEntry(testPlayer);
            Console.WriteLine("Test player ready with guid: " + testPlayer.guid);
        }
    }
}
