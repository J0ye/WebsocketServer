using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using Servertest;

namespace Example
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            Console.WriteLine("Got Message: " + msg);
            string response = msg;
            if(msg == "get")
            {
                response = "New info";
            }
            Send(response);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var wssv = new WebSocketServer(9000);
            wssv.AddWebSocketService<_2DMp>("/2dmp");
            wssv.Start();
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}