using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebsocketServer.Models;

namespace WebsocketServer
{
    class ScoreList : BaseList<Score>
    {
        private static readonly ScoreList instance = new ScoreList();

        static ScoreList() { }

        public ScoreList() { }

        public static ScoreList Instance()
        {
            return instance;
        }

        public string GetTop()
        {
            List<Score> top = new List<Score>();
            int[] scores = new int[List.Count];
            string[] names = new string[List.Count];

            int i = 0;

            foreach(Score v in List)
            {
                scores[i] = v.score;
                names[i] = v.playerName;
                i++;
            }

            System.Array.Sort(scores, names);
            for(int j = scores.Length-1; j >= 0; j--)
            {
                top.Add(new Score(Guid.NewGuid(), names[j], scores[j]));
            }

            string val = "";
            int k = 10;

            foreach (Score v in top)
            {
                val += v.guid + "%" + v.playerName + "%" + v.score + "|";
                k--;
                if (k <= 0) return val;
            }

            return val;
        }

        public string GetListAsString(int count)
        {
            string val = "";
            int i = count;

            foreach (Score v in List)
            {
                val += v.guid + "%" + v.playerName + "%" + v.score + "|";
                i--;
                if (i <= 0) return val;
            }

            return val;
        }
    }
}
