using Microsoft.AspNetCore.Mvc;
using Soccer2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Models
{
    public class TeamProbability
    {
        public string Name { get; set; }

        public double Probability { get; set; }

        public string LastResult { get; set; }

        public double HighestProbability { get; set; }

        public double AverageProbability { get; set; }

        public double CurrentProbability { get; set; }

        public IEnumerable<Game> TeamGames { get; set; }
    }
}
