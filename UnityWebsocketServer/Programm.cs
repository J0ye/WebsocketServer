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
            Console.WriteLine("Got Message: " + msg + ". Returing raw");
            string response = msg;
            Send(e.RawData);
        }
    }
    
    
    public class Program
    {        
        private enum SslProtocolsHack
        {
            Tls = 192,
            Tls11 = 768,
            Tls12 = 3072
        }

        public static void Main(string[] args)
        {
            var wssv = new WebSocketServer(9000, true);
            var wsv = new WebSocketServer(9001);


            wssv.SslConfiguration.ServerCertificate =
                new X509Certificate2("cert.pks");
            var sslProtocolHack = (System.Security.Authentication.SslProtocols)(SslProtocolsHack.Tls12 | SslProtocolsHack.Tls11 | SslProtocolsHack.Tls);
            wssv.SslConfiguration.EnabledSslProtocols = sslProtocolHack;
            wssv.AddWebSocketService<ScoreBehaviour>("/score");
            wsv.AddWebSocketService<ScoreBehaviour>("/score");
            new ScoreList();
            Console.WriteLine("Score Server started");
            Console.WriteLine(".Net Version: {0}", Environment.Version.ToString());
            Console.WriteLine("Press any to stop");
            Console.WriteLine("Add legacy scores");
            ScoreList.Instance().AddEntry(new Score(Guid.NewGuid(), "The Creator", 2017));
            ScoreList.Instance().AddEntry(new Score(Guid.NewGuid(), "st311", 1918));
            ScoreList.Instance().AddEntry(new Score(Guid.NewGuid(), "Cesd", 1018));
            ScoreList.Instance().AddEntry(new Score(Guid.NewGuid(), "felix", 1001));
            ScoreList.Instance().AddEntry(new Score(Guid.NewGuid(), "Frogman", 798));
            ScoreList.Instance().AddEntry(new Score(Guid.NewGuid(), "maggy", 420));
            ScoreList.Instance().AddEntry(new Score(Guid.NewGuid(), "Nice", 69));
            wsv.Start();
            wssv.Start();
            Console.Read();
            wssv.Stop();
            wsv.Stop();
        }
    }
}