using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Soccer2.Data;
using Soccer2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> Games = _db.Games.Where(x => x.Winner == "x");
            return View(Games);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
