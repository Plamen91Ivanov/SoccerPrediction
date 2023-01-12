﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Stats(int id, int page)
        {
            const int PageSize = 25;
            var league = new List<Game>();

            switch (id)
            {
                case 1:
                     league = this.db.Games.Where(x => x.League == "purva liga")
                        .Skip((1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    break;
                case 2:
                     league = this.db.Games.Where(x => x.League == "premier-league")
                        .Skip((1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    break;
                case 3:
                     league = this.db.Games.Where(x => x.League == "championship")
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    break;
                default:
                    break;
            }
            return View(league);
        }

        public IActionResult Teams()
        {
            var teams = this.db.Teams.ToList();
            return View(teams);
        }
    }
}