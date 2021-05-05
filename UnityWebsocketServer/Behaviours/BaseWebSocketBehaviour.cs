using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebsocketServer.Models;
using WebSocketSharp.Server;
using Msg;

namespace WebsocketServer
{
    class BaseWebSocketBehaviour : WebSocketBehavior
    {
        Guid connectionID;
        protected override void OnOpen()
        {
            Player newPlayer = new Player();
            //var msg = "ID: " + newPlayer.guid;
            connectionID = newPlayer.guid;
            IDMessage temp = new IDMessage(newPlayer.guid.ToString());
            Console.WriteLine("New connection. Returned ID: " + newPlayer.guid);
            Send(temp.ToJson());
            Broadcast("Update"); // Ask every user to send an update on their position
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            if(msg == "Ping")
            {
                return;
            }
            Console.WriteLine("Recieved Message: " + msg);
            Broadcast(msg);
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
