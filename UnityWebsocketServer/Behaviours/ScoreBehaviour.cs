using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebsocketServer.Models;
using WebSocketSharp.Server;

namespace WebsocketServer
{
    class ScoreBehaviour : WebSocketBehavior
    {
        Guid connectionID;
        protected override void OnOpen()
        {
            connectionID = Guid.NewGuid();
            var msg = "ID: " + connectionID;
            Console.WriteLine("New connection. Returned ID: " + connectionID);
            Send(msg);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            if (msg.Contains("Score:"))
            {
                Score newScore = Decrypt(msg);
                ScoreList.Instance().RemoveEntry(newScore.guid);
                ScoreList.Instance().AddEntry(newScore);
                Send(ScoreList.Instance().GetTop());
            }
            else if (msg == "Ping")
            {
            }
        }

        public static Score Decrypt(string msg)
        {
            string[] arr = msg.Split("Score:");
            arr = arr[1].Split("%");
            Guid guid = Guid.Parse(arr[0]);
            string playerName = arr[1];
            int score = Convert.ToInt32(arr[2]);
            Score ret = new Score(guid, playerName, score);
            return ret;
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }

        protected new void Send(string data)
        {
            byte[] msg = Encoding.UTF8.GetBytes(data);
            Send(msg);
        }
    }
}
