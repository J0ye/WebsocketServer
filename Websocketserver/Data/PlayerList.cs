using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsocketServer.Models
{
    public sealed class PlayerList : BaseList<Player>
    {
        private static readonly PlayerList instance = new PlayerList();

        static PlayerList() { }

        public PlayerList() { }

        public static PlayerList Instance()
        {
            return instance;
        }

        public override string GetListAsString()
        {
            string val = "";

            foreach (Player v in List)
            {
                val += v.GetPlayerAsString() + "%";
            }

            return val;
        }
        public override string GetListAsString(Guid target)
        {
            string val = "";

            foreach (Player v in List)
            {
                if (v.guid != target)
                {
                    val += v.GetPlayerAsString() + "%";
                }
            }

            return val;
        }
    }
}
