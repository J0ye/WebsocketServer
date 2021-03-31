using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using WebsocketServer.Models;
using WebsocketServer;
using System.Security.Cryptography.X509Certificates;

namespace Example
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            Console.WriteLine("Got Message: " + msg);
            string response = msg;
            Send(response);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var wssv = new WebSocketServer(9000, true);

            wssv.SslConfiguration.ServerCertificate =
                new X509Certificate2("cert.pks");
            wssv.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            wssv.AddWebSocketService<_2DMp>("/2dmp");
            wssv.AddWebSocketService<BaseWebSocketBehaviour>("/base");
            wssv.AddWebSocketService<Echo>("/echo");
            new PlayerList();
            Console.WriteLine("Server started.");
            wssv.Start();
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}