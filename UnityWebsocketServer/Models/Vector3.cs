using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsocketServer.Models;

namespace WebsocketServer
{
    public class Vector3
    {
        public float x = 0;
        public float y = 0;
        public float z = 0;
        public Vector3() { }
        public Vector3(float x, float y) { this.x = x; this.y = y; this.z = 0; }
        public Vector3(float x, float y, float z){ this.x = x; this.y = y; this.z = z; }
    }
}
