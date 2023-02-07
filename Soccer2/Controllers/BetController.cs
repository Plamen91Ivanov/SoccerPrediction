using Microsoft.AspNetCore.Identity;
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
            userManager = this.userManager;
            db = this.db;
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
                Date = Date,
            };
            var addMatch = this.db.BetInfo.Add(betModel);
            this.db.SaveChanges();
            return Content($"Hello {HomeTeam} {AwayTeam} {HomeCoef} {DrawCoef} {AwayCoef} {Bet} {BetType} {CurrentBalance}");
        }
    }
}
