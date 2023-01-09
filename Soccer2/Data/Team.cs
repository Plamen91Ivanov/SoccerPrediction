using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Data
{
    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string League { get; set; }

        public virtual ICollection<Game> HomeGames { get; set; }

        public virtual ICollection<Game> AwayGames { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
