using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Data
{
    public class GameInfo
    {
        public int GameInfoId { get; set; }

        public DateTime Date { get; set; }

        public int HomeResult { get; set; }

        public int AwayResult { get; set; }

        public double HomeCoef { get; set; }

        public double DrawCoef { get; set; }

        public double AwayCoef { get; set; }

        public string Winner { get; set; }


        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string League { get; set; }



    }
}
