﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsocketServer
{
    /// <summary>
    /// Simple version of a 3D vector used to create the position messages.
    /// </summary>
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
