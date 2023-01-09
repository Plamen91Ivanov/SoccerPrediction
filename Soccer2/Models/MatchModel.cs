using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Models
{
    public class MatchModel
    {
        public string HomeTeamId { get; set; }

        public string AwayTeamId { get; set; }

        public int HomeResult { get; set; }

        public int AwayResult { get; set; }

        public DateTime Date { get; set; }

        public double HomeCoef { get; set; }

        public double AwayCoef { get; set; }

        public double DrawCoefficient { get; set; }
    }
}
