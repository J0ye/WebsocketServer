using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsocketServer.Models;

namespace WebsocketServer
{
    public sealed class Vector3List : BaseList<Vector3>
    {
        private static readonly Vector3List instance = new Vector3List();

        static Vector3List() { }

        public Vector3List() { }

        public static Vector3List Instance()
        {
            return instance;
        }

        public override string GetListAsString(Guid target)
        {
            string val = "";
            foreach (Vector3 v in List)
            {
                if (v.guid != target)
                {
                    val += v.x + "!" + v.y + "!" + v.z + "%";
                }
            }

            return val;
        }

        public override string GetListAsString()
        {
            string val = "";

            foreach (Vector3 v in List)
            {
                val += val += v.x + "!" + v.y + "!" + v.z + "%";
            }

            return val;
        }
    }
}
