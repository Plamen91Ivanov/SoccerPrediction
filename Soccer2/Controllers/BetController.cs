﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soccer2.Data;
using Soccer2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Controllers
{
    public class BetController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly ApplicationDbContext db;

        public BetController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.db = db;
        }

        public IActionResult Bet()
        {  
            return View();
        }

        [HttpPost]
        public IActionResult Bet(string HomeTeam,
            string AwayTeam,
            int HomeResult,
            int AwayResult,
            double HomeCoef,
            double DrawCoef,
            double AwayCoef,
            double Bet,
            string BetType,
            double CurrentBalance,
            double BetCoef,
            string ResultStatus,
            string League,
            DateTime Date
            )
        {
            var WinPrice = Bet * BetCoef;
            var betModel = new BetInfo
            {
                HomeTeam = HomeTeam,
                AwayTeam = AwayTeam,
                HomeResult = HomeResult,
                AwayResult = AwayResult,
                HomeCoef = HomeCoef,
                DrawCoef = DrawCoef,
                AWayCoef = AwayCoef,
                Bet = Bet,
                BetType = BetType,
                BetCoef = BetCoef,
                WinPrice = WinPrice,
                ResultStatus = ResultStatus,
                League = League,
                Date = Date,
            };
            this.db.BetInfo.Add(betModel);
            this.db.SaveChanges();
            return View();
        }
    }
}
