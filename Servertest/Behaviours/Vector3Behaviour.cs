using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using WebSocketSharp;



namespace WebsocketServer
{
    class Vector3Behaviour : WebSocketBehavior
    {

        protected override void OnOpen()
        {
            
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
        }
    }
}
