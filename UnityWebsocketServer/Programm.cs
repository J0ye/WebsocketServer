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
    {        private enum SslProtocolsHack
        {
            Tls = 192,
            Tls11 = 768,
            Tls12 = 3072
        }
        public static void Main(string[] args)
        {
            var wssv = new WebSocketServer(9000, true);


            wssv.SslConfiguration.ServerCertificate =
                new X509Certificate2("cert.pks");
            var sslProtocolHack = (System.Security.Authentication.SslProtocols)(SslProtocolsHack.Tls12 | SslProtocolsHack.Tls11 | SslProtocolsHack.Tls);
            wssv.SslConfiguration.EnabledSslProtocols = sslProtocolHack;
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