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
            var getNational = this.db.National
                .Select(n => new NationalModel
                {
                    Name = n.Name,
                    Leagues = n.Leagues.Select(l => new League
                    {
                        Name = l.Name
                    }).ToList(),
                })
                .ToList();

            return View(getNational);
        }

        public IActionResult FutureBet()
        {
            var futureBet = this.db.FutureBet.ToList();
            return View(futureBet);
        }

        public IActionResult AddFutureBet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFutureBet(FutureBetModel futureBetForm)
        {

            var futureBet = new FutureBet
            {
                HomeTeam = futureBetForm.HomeTeam,
                AwayTeam = futureBetForm.AwayTeam,
                League = futureBetForm.League,
                Comment = futureBetForm.Comment,
            };
            this.db.FutureBet.Add(futureBet);
            this.db.SaveChanges();

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
            var Split = League.Split('-');
            var Nation = Split[0];
            var Leagues = Split[1];
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
                Nation = Nation,
                ResultStatus = ResultStatus,
                League = Leagues,
                Date = Date,
                BetTimes = BetTimes,
            };
            this.db.BetInfo.Add(betModel);
            this.db.SaveChanges();
            return RedirectToAction();
        }
    }
}
