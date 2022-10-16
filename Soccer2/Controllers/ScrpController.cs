using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer2.Controllers
{
    public class ScrpController : Controller
    {
        public IActionResult Scrp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Scrp(int date)
        {
            return View();
        }

        public static void Scr()
        {
            Console.OutputEncoding = Encoding.UTF8;

            HtmlWeb web = new HtmlWeb();

            HtmlDocument doc = web.Load($"https://users.pomagalo.com/0,0,0,0,0,0,0,0,0,2,?page=1");
        }

    }
}
