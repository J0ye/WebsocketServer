using System;
using System.Collections.Generic;
using System.Text;
using WebsocketServer.Models;

namespace WebsocketServer
{
    class Score : BaseModel
    {
        public string playerName;
        public int score;

        public Score(Guid id, string name, int newScore)
        {
            playerName = name;
            guid = id;
            score = newScore;
        }
    }
}
