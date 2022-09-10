using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using WebsocketServer;
using System.Security.Cryptography.X509Certificates;
     
/// <summary>
/// Contains the Main methode, which start the server instances 
/// </summary>
public class Program
{       
    public static void Main(string[] args)
    {
        // Create a websocket server for secure connections 
        var wssv = new WebSocketServer(9000, true);
        // Create a websocket server for native implementations that dont need a secure connection
        var wsv = new WebSocketServer(9001);

        // Add the SSL configuration to the secure websocket server
        wssv.SslConfiguration.ServerCertificate = new X509Certificate2("cert.pks");
        wssv.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
        wssv.AddWebSocketService<BaseWebSocketBehaviour>("/base");
        wssv.AddWebSocketService<ChatBehaviour>("/chat");
        wsv.AddWebSocketService<BaseWebSocketBehaviour>("/base");
        wsv.AddWebSocketService<ChatBehaviour>("/chat");

        // extra channels that are all based on the basebehaviour. Add an instance for bot variants of the websocket server.
        wssv.AddWebSocketService<BaseWebSocketBehaviour>("/alpha");
        wsv.AddWebSocketService<BaseWebSocketBehaviour>("/alpha");
        wssv.AddWebSocketService<BaseWebSocketBehaviour>("/beta");
        wsv.AddWebSocketService<BaseWebSocketBehaviour>("/beta");
        wssv.AddWebSocketService<BaseWebSocketBehaviour>("/gamma");
        wsv.AddWebSocketService<BaseWebSocketBehaviour>("/gamma");
        wssv.AddWebSocketService<BaseWebSocketBehaviour>("/delta");
        wsv.AddWebSocketService<BaseWebSocketBehaviour>("/delta");
        wssv.AddWebSocketService<BaseWebSocketBehaviour>("/omega");
        wsv.AddWebSocketService<BaseWebSocketBehaviour>("/omega");

        Console.WriteLine("Server started");
        Console.WriteLine(".Net Version: {0}", Environment.Version.ToString());
        Console.WriteLine("Running 6 servers on port:" + wssv.Port);

        wsv.Start();
        wssv.Start();
        if (Console.ReadKey().Key == ConsoleKey.Enter)
        {
            // Shut the server down, if a button has been pressed
            wssv.Stop();
            wsv.Stop();
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