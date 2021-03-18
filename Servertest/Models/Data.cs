using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servertest;

namespace Servertest.Models
{
    public sealed class Data
    {
        private static readonly Data instance = new Data();
        private List<Player> players = new List<Player>();
        private List<Vector3> vector3s = new List<Vector3>();

        static Data() { }

        public Data() { }

        public static Data Instance()
        {
            return instance;
        }

        public void AddPlayer(Player newPlayer)
        {
            players.Add(newPlayer);
        }

        public bool RemovePlayer(Guid target)
        {
            foreach (Player p in players)
            {
                if (p.id == target)
                {
                    players.Remove(p);
                    return true;
                }
            }
            Console.WriteLine("Error: The server is trying to delete a player that does not exist.");
            return false;
        }

        public List<Player> GetPlayers()
        {
            return players;
        }
        
        // Returns a list of every player but one target player, as a string
        public string GetPlayerListAsString(Guid target)
        {
            string val = "";
            foreach (Player p in players)
            {
                if (p.id != target)
                {
                    val += p.GetPlayer() + "%";
                }
            }

            return val;
        }

        // Returns the list of all the players as a string
        public string GetPlayerListAsString()
        {
            string val = "";

            Console.WriteLine("List of Players: ");
            foreach (Player p in players)
            {
                Console.WriteLine("Player: " + p.id);
                val += p.GetPlayer() + "%";
            }

            return val;
        }
    }
}
