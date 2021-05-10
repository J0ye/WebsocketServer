using System;
using Newtonsoft.Json;
using WebsocketServer.Models;

namespace Msg
{
    public enum WebsocketMessageType { Request, Position, Chat, ID, SyncString, SyncFloat, RPC }
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

    public class WebsocketRequest : IDMessage
    {
        public WebsocketMessageType requestType;

        public WebsocketRequest()
        {
            guid = Guid.NewGuid().ToString();
            type = WebsocketMessageType.Request;
        }

        public WebsocketRequest(WebsocketMessageType request, Guid id)
        {
            guid = id.ToString();
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

    public class IDMessage : WebsocketMessage
    {
        public string guid;

        public IDMessage()
        {
            type = WebsocketMessageType.ID;
        }

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

        public void SetGuid(string target)
        {
            if (Guid.TryParse(target, out Guid temp))
            {
                guid = target;
            }
            else
            {
                throw new Exception("Script is trying to set the id of a PositionMessage to an invalid guid");
            }
        }
    }

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
            this.guid = id.ToString();
            this.callName = callName;
            this.stringValue = stringValue;
            this.floatValue = floatValue;
        }
    }
}
