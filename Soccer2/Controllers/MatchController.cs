using Microsoft.AspNetCore.Mvc;
using Soccer2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Controllers
{
    public class MatchController : Controller
    {
        private readonly ApplicationDbContext db;

        public MatchController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Matches()
        {
            var Today = DateTime.Today.ToString("s").Split('T')[0];

            var matches = this.db.TestDateTime
                .Where(x => x.Date == Today)
                .OrderBy(x => x.Time)
                .ToList();

            var Todayy = DateTime.Today;
            var testt = DateTime.Today;
            //2022 - 11 - 08
            var test = this.db.Games.Where(x => x.Date.Date == Todayy).ToList();
            var test1 = this.db.Games.Where(x => x.Date.Date == testt).ToList();
            return View(matches);
        }
    }
}
