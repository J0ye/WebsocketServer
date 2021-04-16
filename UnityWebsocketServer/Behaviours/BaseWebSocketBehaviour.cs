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
        Guid connectionID;
        protected override void OnOpen()
        {
            Player newPlayer = new Player();
            var msg = "ID: " + newPlayer.guid;
            connectionID = newPlayer.guid;
            Console.WriteLine("New connection. Returned ID: " + newPlayer.guid);
            Send(msg);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            if (msg.Contains("Pos:"))
            {
                Broadcast(msg);
            }
            else if (msg.Contains("Get guid"))
            {
                Player newPlayer = new Player();
                PlayerList.Instance().AddEntry(newPlayer);
                var ret = "ID: " + newPlayer.guid;
                Console.WriteLine("New id for existing connection: " + newPlayer.guid);
                Send(ret);
            }
            else if(msg == "Ping")
            {
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            PlayerList.Instance().RemoveEntry(connectionID);
            string exitPosition = "Pos:" + (-9999) + "/" + (-9999) + "/" + (-9999) + "/" + connectionID;
            Broadcast(exitPosition); // Tell every open connection to move the chracter to the exit position. It will be deleted after that.
            base.OnClose(e);
        }

        protected new void Send(string data)
        {
            byte[] msg = Encoding.UTF8.GetBytes(data);
            Send(msg);
        }

        protected void Broadcast(string data)
        {
            byte[] msg = Encoding.UTF8.GetBytes(data);
            Sessions.Broadcast(msg);
        }
    }
}
