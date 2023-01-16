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
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IStatisticsService statistics;
        public StatisticsController(ApplicationDbContext db, IStatisticsService statistics)
        {
            this.db = db;
            this.statistics = statistics;
        }
        public IActionResult Stats(string league, int page, int id)
        {
            const int PageSize = 25;
            
            var teamLeague = this.statistics.TeamByLeague(league);

            if (league != null)
            {
                 this.statistics.TeamByLeagueStatistics(league);
            }
 
            //switch (id)
            //{
            //    case 1:
            //         league = this.db.Games.Where(x => x.League == "purva liga")
            //            .Skip((1) * PageSize)
            //            .Take(PageSize)
            //            .ToList();
            //        break;
            //    case 2:
            //         league = this.db.Games.Where(x => x.League == "premier-league")
            //            .Skip((1) * PageSize)
            //            .Take(PageSize)
            //            .ToList();
            //        break;
            //    case 3:
            //         league = this.db.Games.Where(x => x.League == "championship")
            //            .Skip((page - 1) * PageSize)
            //            .Take(PageSize)
            //            .ToList();
            //        break;
            //    default:
            //        break;
            //}
            return View(teamLeague);
        }

        public IActionResult Teams()
        {
            var teams = this.db.Teams.ToList();
            return View(teams);
        }

        public IActionResult Team(string league)
        {
            var teamName = league;
            var statistics = this.statistics.TeamStatsSortExcersise(teamName);

            return View(statistics);
        }
    }   
}
