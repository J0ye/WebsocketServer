using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsocketServer.Models;

namespace WebsocketServer
{
    public class Player : BaseModel
    {
        public Vector3 pos;

        public Player()
        {
            guid = Guid.NewGuid();
            pos = new Vector3();
        }

        public void SetPos(float x, float y, float z)
        {
            pos.x = x;
            pos.y = y;
            pos.z = z;
        }

        public string GetPlayerAsString()
        {
            string _id = guid.ToString();
            string _pos = pos.x + "/" + pos.y + "/" + pos.z;
            return _id + "!" + _pos;
        }
    }
}
