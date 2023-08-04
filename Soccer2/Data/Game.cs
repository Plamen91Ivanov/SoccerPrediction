using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Data
{
    public class Game
    { 
        public int GameId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int HomeResult { get; set; }

        public int AwayResult { get; set; }

        public double HomeCoef { get; set; }

        public double DrawCoef { get; set; }

        public double AwayCoef { get; set; }

        public string Winner { get; set; }


        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public string League { get; set; }

        public virtual Team HomeTeam { get; set; }

        public virtual Team AwayTeam { get; set; }

    }
}
