using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Team(string league)
        {
            var test = league;
            return View();
        }
    }
}
