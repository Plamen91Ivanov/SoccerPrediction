﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Models
{
    public class BetInfoModel
    {
        public int BetInfoId { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeResult { get; set; }

        public int AwayResult { get; set; }

        public int HomeResultHalfTime { get; set; }

        public int AwayResultHalfTime { get; set; }

        public string Result { get; set; }

        public string ResultHT { get; set; }

        public double HomeCoef { get; set; }

        public double DrawCoef { get; set; }

        public double AWayCoef { get; set; }

        public string Winner { get; set; }

        public string Comment { get; set; }

        public double Bet { get; set; }

        public double BetCoef { get; set; }

        public string BetType { get; set; }

        public double WinPrice { get; set; }

        public string League { get; set; }
        
        public string ResultStatus { get; set; }

        public DateTime Date { get; set; }
    }
}
