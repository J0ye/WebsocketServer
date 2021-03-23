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
    class _2DMp : BaseWebSocketBehaviour
    {
        // Message Format( Pos: Xpos/Ypos/id )
        protected override void SetNewPos(string msg)
        {
            var stringArray = msg.Split(":".ToCharArray());
            stringArray = stringArray[1].Split("/".ToCharArray());
            string x = stringArray[0];
            string y = stringArray[1];
            string id = stringArray[2];
            //Console.WriteLine("After second split: x:" + x + ", y:" + y + " ID: " + id);
            Guid guid = Guid.Parse(id);
            Player target = PlayerList.Instance().FindEntry(guid);
            float xPos = float.Parse(x);
            float yPos = float.Parse(y);
            target.SetPos(xPos, yPos, 0);
        }
    }
}
