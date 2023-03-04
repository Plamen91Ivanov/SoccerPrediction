using Microsoft.AspNetCore.Mvc;
using Soccer2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Controllers
{
    public class NationalController : Controller
    {
        private readonly ApplicationDbContext db;

        public NationalController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult AddLeague()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLeague(string National, string League)
        {
            var getNational = this.db.National.Where(x => x.Name == National).FirstOrDefault();

            if (getNational == null)
            {
                var addNational = new National
                {
                    Name = National,
                };

                this.db.National.Add(addNational);
                this.db.SaveChanges();
                getNational = this.db.National.Where(x => x.Name == National).FirstOrDefault();
            }

            var addLeague = new League
            {
                Name = League,
                NationalId = getNational.Id,
            };
            this.db.League.Add(addLeague);
            this.db.SaveChanges();

            return RedirectToAction("AddBet", "Bet");
        }
    }
}

