using Microsoft.AspNetCore.Mvc;
using Soccer2.Data;
using Soccer2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext db;

        public TestController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Test()
        {
            IEnumerable<TeamModel> Teams = db
                .Teams
                .Select(t => new TeamModel
                {
                    Name = t.Name,
                    AwayGames = t.AwayGames.Select(g => new Game
                    {
                        HomeResult = g.HomeResult,
                        AwayResult = g.AwayResult,
                        Date = g.Date,
                        Winner = g.Winner,
                        AwayCoef = g.AwayCoef,
                        DrawCoef = g.DrawCoef,
                        HomeCoef = g.HomeCoef,
                        HomeTeamName = g.HomeTeamName,
                        AwayTeamName = g.AwayTeamName

                    }),
                    HomeGames = t.HomeGames.Select(g => new Game
                    {
                        HomeResult = g.HomeResult,
                        AwayResult = g.AwayResult,
                        Date = g.Date,
                        Winner = g.Winner,
                        AwayCoef = g.AwayCoef,
                        DrawCoef = g.DrawCoef,
                        HomeCoef = g.HomeCoef,
                        HomeTeamName = g.HomeTeamName,
                        AwayTeamName = g.AwayTeamName
                    })
                });

            return View(Teams);
        }
    }
}
