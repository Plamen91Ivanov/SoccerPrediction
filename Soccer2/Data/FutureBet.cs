using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Data
{
    public class FutureBet
    {
        public int Id { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string League { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public int Probability { get; set; }

        public double Coefficient { get; set; }
    }
}
