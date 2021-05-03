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
            wssv.AddWebSocketService<_2DMp>("/2dmp");
            wssv.AddWebSocketService<BaseWebSocketBehaviour>("/base");
            wssv.AddWebSocketService<Echo>("/echo");
            wssv.AddWebSocketService<ChatBehaviour>("/chat");
            wsv.AddWebSocketService<BaseWebSocketBehaviour>("/base");
            wsv.AddWebSocketService<Echo>("/echo");
            wsv.AddWebSocketService<ChatBehaviour>("/chat");
            new PlayerList();
            Console.WriteLine("Server started");
            Console.WriteLine(".Net Version: {0}", Environment.Version.ToString());
            Console.WriteLine("Press " + ConsoleKey.Enter + " to cancel");
            wsv.Start();
            wssv.Start();
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                wssv.Stop();
                wsv.Stop();
            }
        }
    }
}

/*
https://github.com/sta/websocket-sharp/issues/356 
I don't know if this will help in practice or not, but you should check WebSocket.SslConfiguration.EnabledSslProtocols. This resolves as type System.Security.Authentication.SslProtocols which is an enum:

[Flags]
public enum SslProtocols
{
    None = 0,
    Ssl2 = 12,
    Ssl3 = 48,
    Tls = 192,
    Default = 240,
    Tls11 = 768,
    Tls12 = 3072
}
If you examine the documentation for Default, you will see:

Specifies that either Secure Sockets Layer (SSL) 3.0 or Transport Layer Security 1.0 are acceptable for secure communications

I imagine if you override the Default value and set it to Tls12 before connect (or better: Default | Tls11 | Tls12), you might obtain the results you want/expect.

Note that the value of Default in this case is coming from .NET, not from this project, so if this does resolve your problem then it might be worth creating an upstream issue to change the value of Default, though I'm sure it will happen on its own eventually*/