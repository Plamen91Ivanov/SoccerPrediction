using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soccer2.Data;
using Soccer2.Models;
using Soccer2.Services;
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

        private readonly IBetService bet;

        public BetController(ApplicationDbContext db, UserManager<IdentityUser> userManager, IBetService bet)
        {
            this.userManager = userManager;
            this.db = db;
            this.bet = bet;
        }

        public IActionResult Bet()
        {
            var bets = this.bet.All();
            return View(bets);
        }

        public IActionResult AddBet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBet(string HomeTeam,
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
            int BetTimes,
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
                BetTimes = BetTimes,
            };
            this.db.BetInfo.Add(betModel);
            this.db.SaveChanges();
            return RedirectToAction();
        }
    }
}
