using System;
using Newtonsoft.Json;

/// <summary>
/// This namespace is a collection of different message classes so messages can be created and converted from and to JSON. 
/// </summary>
namespace Msg
{
    /// <summary>
    /// This type is used to identify the goal of a message without reading it. 
    /// </summary>
    public enum WebsocketMessageType { Request, Position, Chat, ID, SyncString, SyncFloat, RPC }

    /// <summary>
    /// Base class for all other messages. Has only a member for the type. Used to decode incoming message and to read the type without actually knowing it.
    /// </summary>
    public class WebsocketMessage
    {
        public WebsocketMessageType type;

        public static WebsocketMessage FromJson(string target)
        {
            return JsonConvert.DeserializeObject<WebsocketMessage>(target);
        }

        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    /// <summary>
    /// Inherits the websocket class and adds a member for the ID as a string. Is used to adress a message with a user id.
    /// </summary>
    public class IDMessage : WebsocketMessage
    {
        public string connectionID;

        public IDMessage()
        {
            type = WebsocketMessageType.ID;
        }
        /// <summary>
        /// Constructor to build message with a guid as a string.
        /// </summary>
        /// <param name="guid"></param>
        public IDMessage(string guid)
        {
            type = WebsocketMessageType.ID;
            SetGuid(guid);
        }

        public static new IDMessage FromJson(string target)
        {
            JsonConvert.DeserializeObject<IDMessage>(target);
            return JsonConvert.DeserializeObject<IDMessage>(target);
        }

        public override string ToJson()
        {
            JsonConvert.SerializeObject(this);
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Only allows a string that can be parsed into a Guid to be set as the connectionID.
        /// </summary>
        /// <param name="target">The new conncetionID as a string.</param>
        public void SetGuid(string target)
        {
            if (Guid.TryParse(target, out Guid temp))
            {
                connectionID = target;
            }
            else
            {
                throw new Exception("Script is trying to set the id of a PositionMessage to an invalid guid");
            }
        }
    }

    /// <summary>
    /// Special form of the websocket message to request an update of a certain type. The certain type is saved in the requestType member. 
    /// </summary>
    public class WebsocketRequest : IDMessage
    {
        /// <summary>
        /// Websocketmessage type that is requested.
        /// </summary>
        public WebsocketMessageType requestType;

        /// <summary>
        /// Empty constructor signs the messag with a new random id. Since this message is from the server and goes out to all, id does not matter.
        /// </summary>
        public WebsocketRequest()
        {
            connectionID = Guid.NewGuid().ToString();
            type = WebsocketMessageType.Request;
        }

        public WebsocketRequest(WebsocketMessageType request, Guid id)
        {
            connectionID = id.ToString();
            type = WebsocketMessageType.Request;
            requestType = request;
        }

        public new static WebsocketRequest FromJson(string target)
        {
            return JsonConvert.DeserializeObject<WebsocketRequest>(target);
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// This class is used to transfer position data in the form of a 3D vector between open connections. Also used to notify other connections when a user closes their connection.
    /// </summary>
    public class PositionMessage : IDMessage
    {
        public WebsocketServer.Vector3 position;

        public PositionMessage()
        {
            type = WebsocketMessageType.Position;
        }
        public PositionMessage(Guid id, WebsocketServer.Vector3 pos)
        {
            type = WebsocketMessageType.Position;
            SetGuid(id.ToString());
            position = pos;
        }

        public PositionMessage(string id, WebsocketServer.Vector3 pos)
        {
            type = WebsocketMessageType.Position;
            SetGuid(id);
            position = pos;
        }

        public static new PositionMessage FromJson(string target)
        {
            return JsonConvert.DeserializeObject<PositionMessage>(target);
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class SyncVarMessage : IDMessage
    {
        public string callName;
        public string stringValue;
        public float floatValue;

        public SyncVarMessage(WebsocketMessageType targetType, Guid id, string callName, string stringValue, float floatValue)
        {
            if (targetType != WebsocketMessageType.SyncFloat && targetType != WebsocketMessageType.SyncString)
                throw new Exception("Script is trying to create a SyncVarMessage for " + callName + " without the proper type");

            this.type = targetType;
            this.connectionID = id.ToString();
            this.callName = callName;
            this.stringValue = stringValue;
            this.floatValue = floatValue;
        }
    }
}
