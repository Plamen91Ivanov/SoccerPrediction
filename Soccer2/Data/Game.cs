using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Data
{
    public class Game
    {
        public Game()
        {

        }

        public int GameId { get; set; }

        public DateTime Date { get; set; }

        public int HomeResult { get; set; }

        public int AwayResult { get; set; }


        public double HomeCoef { get; set; }

        public double AwayCoef { get; set; }


        public string Winner { get; set; }


        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }


        public virtual Team HomeTeam { get; set; }

        public virtual Team AwayTeam { get; set; }
    }
}
