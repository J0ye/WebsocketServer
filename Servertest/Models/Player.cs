using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servertest.Models;

namespace Servertest
{
    public class Player
    {
        public Guid id;
        public Vector3 pos;

        public Player()
        {
            id = Guid.NewGuid();
            pos = new Vector3();
        }

        public void SetPos(float x, float y, float z)
        {
            pos.x = x;
            pos.y = y;
            pos.z = z;
        }

        public string GetPlayer()
        {
            string _id = id.ToString();
            string _pos = pos.x + "/" + pos.y + "/" + pos.z;
            return _id + "!" + _pos;
        }
    }
}
