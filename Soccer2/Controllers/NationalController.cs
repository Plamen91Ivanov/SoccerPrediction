using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Controllers
{
    public class NationalController : Controller
    {
        public IActionResult AddLeague()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLeague(string National, string League)
        {
            return View();
        }
    }
}

//RedirectToAction("Bet", "AddBet");