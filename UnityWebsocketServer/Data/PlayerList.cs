using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsocketServer
{
    /// <summary>
    /// Implements the base list as a player list for the player class. Also makes it a singelton.
    /// </summary>
    public sealed class PlayerList : BaseList<Player>
    {
        private static readonly PlayerList instance = new PlayerList();

        static PlayerList() { }

        public PlayerList() { }

        public static PlayerList Instance()
        {
            return instance;
        }
    }
}
