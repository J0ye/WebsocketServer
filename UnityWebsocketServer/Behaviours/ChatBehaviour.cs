using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Text;

namespace WebsocketServer
{
    class ChatBehaviour : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            global.Instance().g_ChatMember++;
            Console.WriteLine("New user in chat. Number of users " + global.Instance().g_ChatMember);
            NotifyNumberOfUsers("entered");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            if (msg != "Ping")
            {
                Console.WriteLine("Recieved chat message: " + msg);
                Sessions.Broadcast(ToByteArray(msg));
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            global.Instance().g_ChatMember--;
            Console.WriteLine("User left. Number of users " + global.Instance().g_ChatMember);
            NotifyNumberOfUsers("left");
        }

        protected void NotifyNumberOfUsers(string verb)
        {
            var msg = "User " + verb + " Number of users " + global.Instance().g_ChatMember;
            Sessions.Broadcast(ToByteArray(msg));
        }

        protected byte[] ToByteArray(string data)
        {
            byte[] msg = Encoding.UTF8.GetBytes(data);
            return msg;
        }
    }
}
