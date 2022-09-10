using System;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using Msg;

namespace WebsocketServer
{
    /// <summary>
    /// this class is the main websocketbehaviour used for the hybrid peer to peer. Messages are broadcast to every open connection, if the message fits the criteria.
    /// </summary>
    class BaseWebSocketBehaviour : WebSocketBehavior
    {
        /// <summary>
        /// There is an instance of this script for every open connection. This variable is the connection ID the server created and returned to the user.
        /// </summary>
        Guid connectionID;
        protected override void OnOpen()
        {
            Player newPlayer = new Player();
            //var msg = "ID: " + newPlayer.guid;
            connectionID = newPlayer.guid;
            IDMessage temp = new IDMessage(newPlayer.guid.ToString());
            Console.WriteLine("New connection. Returned ID: " + newPlayer.guid);
            Send(temp.ToJson());
            WebsocketRequest req = new WebsocketRequest(WebsocketMessageType.Position, connectionID);
            Console.WriteLine("Broadcasting new conncetion");
            Broadcast(req.ToJson()); // Ask every user to send an update on their position
        }

        /// <summary>
        /// Event that happens when a new message from an user is recieved.
        /// This function overides the OnMessage event of the most basic websocket behaviour. It will extract the message from the message event args and broadcast it to all open connections.
        /// </summary>
        /// <param name="e">The contents and meta data of a new message as event args</param>
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            if(msg == "Ping")
            {
                return;
            }
            Console.WriteLine("Recieved Message: " + msg);
            Broadcast(msg);
        }

        /// <summary>
        /// Event that happens when a connection is closed.
        /// This function creates a message for all open connections to move the user, that closed this connection, to (-9999|-9999|-9999). The clients will recognize this position and the delete the avatar in their instance.
        /// </summary>
        /// <param name="e">data about the connetion that is being closed as special CloseEventArgs</param>
        protected override void OnClose(CloseEventArgs e)
        {
            Msg.PositionMessage temp = new Msg.PositionMessage(connectionID, new Vector3(-9999, -9999, -9999));
            Broadcast(temp.ToJson()); // Tell every open connection to move the chracter to the exit position. It will be deleted after that.
            Console.WriteLine("Closing connection with " + connectionID);
            base.OnClose(e);
        }

        /// <summary>
        /// Creates a new, different function called Send that calls the base function send of the parent class, but also encodes a message as a string into a byte array to send it. 
        /// </summary>
        /// <param name="data">The message as a string</param>
        protected new void Send(string data)
        {
            byte[] msg = Encoding.UTF8.GetBytes(data);
            base.Send(msg);
        }

        /// <summary>
        /// Encodes a message as a string into an array of bytes to send to all open connections, similar to the Send function
        /// </summary>
        /// <param name="data">The message as a string</param>
        protected void Broadcast(string data)
        {
            byte[] msg = Encoding.UTF8.GetBytes(data);
            Sessions.Broadcast(msg);
        }
    }
}
