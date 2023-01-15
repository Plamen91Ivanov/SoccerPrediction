using Soccer2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Models
{
    public class TeamModel
    {
        public string Name { get; set; }

        public string League { get; set; }

        public IEnumerable<Game> HomeGames { get; set; }

        public IEnumerable<Game> AwayGames { get; set; }

        public IEnumerable<Game> Games { get; set; }

        public List<Game> Matches { get; set; }

        public int  GamesNumber { get; set; }
    }
}
