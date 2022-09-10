using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsocketServer
{
    /// <summary>
    /// Base for all models that implements an id as a guid to adress models to open connections. 
    /// </summary>
    public class BaseModel
    {
        public Guid guid;
    }

    /// <summary>
    /// Model used to create lists of users and positions.
    /// </summary>
    public class Player : BaseModel
    {
        public Vector3 position;

        public Player()
        {
            guid = Guid.NewGuid();
            position = new Vector3();
        }

        public void SetPosition(float x, float y, float z)
        {
            position.x = x;
            position.y = y;
            position.z = z;
        }
    }
}
