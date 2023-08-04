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
            var Today = DateTime.Today;
            var matches = this.db.Games.Where(x=> x.Date == Today).ToList();

            return View(matches);
        }
    }
}
