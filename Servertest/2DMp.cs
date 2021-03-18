using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using Servertest.Models;
using WebSocketSharp.Server;

namespace Servertest
{
    class _2DMp : WebSocketBehavior
    {

        protected override void OnOpen()
        {
            Player newPlayer = new Player();
            //players.Add(newPlayer);
            Models.Data.Instance().AddPlayer(newPlayer);
            var msg = "ID: " + newPlayer.id;
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
            else if(msg.Contains("Get:"))
            {
                ReturnData(msg);
            }
            else if (msg.Contains("Delete:"))
            {
                DeletePlayer(msg);
            }
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
            target.SetPos(xPos, yPos, 0);
        }

        protected void ReturnData(string msg)
        {
            Guid guid = ParseGuidDefault(msg);

            Send("Players:" + Models.Data.Instance().GetPlayerListAsString(guid));
        }

        protected void DeletePlayer(string msg)
        {
            Guid guid = ParseGuidDefault(msg);
            Models.Data.Instance().RemovePlayer(guid);
        }

        protected Player FindPlayer(Guid id)
        {
            foreach(Player p in Models.Data.Instance().GetPlayers())
            {
                if(p.id == id)
                {
                    return p;
                }
            }
            Console.WriteLine("Error: The player with the id: " + id + " does not exist but the programm is still trying to find him.");
            return Models.Data.Instance().GetPlayers()[0];
        }

        // Parses a string  to a guid and returns. Only works for a string with this format: Command:Guid
        // The command will be ignored
        protected Guid ParseGuidDefault(string msg)
        {
            var stringArray = msg.Split(":".ToCharArray());
            Guid guid = Guid.Parse(stringArray[1]);
            return guid;
        }
    }
}
