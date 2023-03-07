using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PuppeteerSharp;
using Soccer2.Data;
using Soccer2.Models;

namespace Soccer2.Controllers
{
    public class ScrpController : Controller
    {
        private readonly ApplicationDbContext db;

        public ScrpController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Scrp()
        {
            Scr();
            return View();
        }

        [HttpPost]
        public IActionResult Scrp(int date)
        {
            return View();
        }

        public async void ScrTeams()
        {
            Console.OutputEncoding = Encoding.UTF8;

            HtmlWeb web = new HtmlWeb();

            HtmlDocument doc = web.Load($"https://footystats.org/england/efl-league-two");
            //var HeaderNames = doc.DocumentNode.SelectNodes("//div[contains(@class, 'overflow__in')]");
            //var HeaderNames1 = doc.DocumentNode.SelectNodes("//div[contains(@class, 'box-overflow')]");
            //var HeaderNames2 = doc.DocumentNode.SelectNodes("//td[contains(@class, 'table-main__odds')]");
            //var HeaderNames4 = doc.DocumentNode.SelectNodes("//tr[contains(@class, 'table-main__played')]");
            //var HeaderNames5 = doc.DocumentNode.SelectNodes("//tbody");
            //var HeaderNames6 = doc.DocumentNode.SelectNodes("//tbody//tr");
           // var HeaderNames = doc.DocumentNode.SelectNodes("//tr"); team_name_span
            var HeaderNames = doc.DocumentNode.SelectNodes("//a[contains(@class, 'hover-modal-ajax-team')]");

            for (int i = 0; i < HeaderNames.Count; i++)
            {
                var teamName = HeaderNames[i].InnerHtml.Split("FC")[0].Trim();
                var league = "efl league two";

                var teamModel = new Team
                {
                    Name = teamName,
                    League = league
                };

                this.db.Teams.Add(teamModel);
                db.SaveChanges();
            }
        }

        public async void Scr()
        {
            //htmlagilipack example 
            
            Console.OutputEncoding = Encoding.UTF8;

            HtmlWeb web = new HtmlWeb();

            var country = "england";
            var league = "premier-league";
            //HtmlDocument doc = web.Load($"https://www.betexplorer.com/soccer/england/premier-league/results/");

            HtmlDocument doc = web.Load($"https://www.betexplorer.com/soccer/{country}/{league}/results/");
            //var HeaderNames = doc.DocumentNode.SelectNodes("//div[contains(@class, 'overflow__in')]");
            //var HeaderNames1 = doc.DocumentNode.SelectNodes("//div[contains(@class, 'box-overflow')]");
            //var HeaderNames2 = doc.DocumentNode.SelectNodes("//td[contains(@class, 'table-main__odds')]");
            //var HeaderNames4 = doc.DocumentNode.SelectNodes("//tr[contains(@class, 'table-main__played')]");
            //var HeaderNames5 = doc.DocumentNode.SelectNodes("//tbody");
            //var HeaderNames6 = doc.DocumentNode.SelectNodes("//tbody//tr");
            var HeaderNames = doc.DocumentNode.SelectNodes("//tr");

            //get last match date 
            //var getLastMatch = this.db.Games.Where(x => x.League == league).OrderByDescending(x => x.Date).FirstOrDefault();

            DateTime lastMatchDate = new DateTime();
             
            for (int i = 1; i < HeaderNames.Count -1; i++)
            {

                if (HeaderNames[i].ChildNodes.Count == 6)
                {
                var MatchDetails = HeaderNames[i].ChildNodes;
                var TeamsSplit = MatchDetails[0].InnerText.Split('-'); // split by -
                var HomeTeam = TeamsSplit[0].Trim();
                var AwayTeam = TeamsSplit[1].Trim();
                var ResultSplit = MatchDetails[1].InnerText.Split(':'); // split by :
                    if (ResultSplit.Length == 1)
                    {
                        continue;
                    }
                var HomeResult = Convert.ToInt32(ResultSplit[0]);
                var AwayResult = Convert.ToInt32(ResultSplit[1]);
                var HomeCoefSplit = Convert.ToDouble(MatchDetails[2]
                    .OuterHtml
                    .Split("data-odd")[1]
                    .Split('>')[0]
                    .Split('\"')[1]);

                var DrawCoefSplit = Convert.ToDouble(MatchDetails[3].OuterHtml.Split("data-odd")[1].Split('>')[0].Split('\"')[1]); // x coef same
                var AwayCoefSplit = Convert.ToDouble(MatchDetails[4].OuterHtml.Split("data-odd")[1].Split('>')[0].Split('\"')[1]); // coef same 
                var DateSplit = MatchDetails[5].OuterHtml.Split('>')[1].Split('>')[0].Split('>')[0].Split('<')[0]; // this is mby date >after that [0]

                    var swapDayAndMonth = DateSplit.Split('.');
                    var month = int.Parse(swapDayAndMonth[1]);
                    var swapDay = "";
                    if (month < 2)
                    {
                         swapDay = swapDayAndMonth[1] + '.' + swapDayAndMonth[0];
                    }
                    else
                    {
                         swapDay = "2022" + '.' + swapDayAndMonth[1] + '.' + swapDayAndMonth[0];
                    }

                    var parseDate = DateTime.Parse(swapDay);

                    if (parseDate > lastMatchDate)
                    {

                    var Winner = "";

                    if (HomeResult == AwayResult)
                    {
                        Winner = "x";
                    }
                    else if( HomeResult < AwayResult)
                    {
                        Winner = AwayTeam;
                    }
                    else
                    {
                        Winner = HomeTeam;
                    }

                    DateTime date;

                    if (DateSplit == "Yesterday")
                    {
                        date = DateTime.Today;
                    }
                    else if(DateSplit == "Today")
                    {
                        date = DateTime.Today;
                    }
                    else 
                    {
                        var t = DateSplit.Split('.');
                            var ReversedDate = "";
                            if (t[2] != "2022")
                            {
                                 ReversedDate = $"{t[1]}.{t[0]}";
                            }
                            else
                            {
                                 ReversedDate = $"{t[2]}.{t[1]}.{t[0]}";
                            }
                            date = DateTime.Parse(ReversedDate);
                    }

                    var getHomeTeamId = db.Teams.Where(x => x.Name == HomeTeam).ToArray();
                    var getAwayTeamId = db.Teams.Where(x => x.Name == AwayTeam).ToArray();

                    if (getHomeTeamId.Length != 0 && getAwayTeamId.Length != 0)
                    {
                        var MatchModel = new Game
                        {
                            HomeTeamName = HomeTeam,
                            AwayTeamName = AwayTeam,
                            HomeTeamId = getHomeTeamId[0].TeamId,
                            AwayTeamId = getAwayTeamId[0].TeamId,
                            HomeResult = HomeResult,
                            AwayResult = AwayResult,
                            HomeCoef = HomeCoefSplit,
                            AwayCoef = AwayCoefSplit,
                            DrawCoef = DrawCoefSplit,
                            Date = date,
                            Winner = Winner,
                            League = league
                        };

                        var test = this.db.Games.Add(MatchModel);
                        db.SaveChanges();
                    }
                    else
                    {
                        ;
                    }
                    }

                }

                //int result = umodel.SaveDetails();
                //if (result > 0)
                //{
                //    ViewBag.Result = "Data Saved Successfully";
                //}
                //else
                //{
                //    ViewBag.Result = "Something Went Wrong";
                //}
            }

        }

    }
}

//switch (HomeTeam)
//{
//    case "CSKA Sofia":
//        homeTeamId = 6;
//        break;
//    case "Ludogorets":
//        homeTeamId = 7;
//        break;
//    case "CSKA 1948 Sofia":
//        homeTeamId = 8;
//        break;
//    case "Lok. Plovdiv":
//        homeTeamId = 9;
//        break;
//    case "Slavia Sofia":
//        homeTeamId = 10;
//        break;
//    case "Cherno More":
//        homeTeamId = 11;
//        break;
//    case "Levski Sofia":
//        homeTeamId = 12;
//        break;
//    case "Lok. Sofia":
//        homeTeamId = 13;
//        break;
//    case "Arda":
//        homeTeamId = 14;
//        break;
//    case "Botev Plovdiv":
//        homeTeamId = 15;
//        break;
//    case "Beroe":
//        homeTeamId = 16;
//        break;
//    case "Septemvri Sofia":
//        homeTeamId = 17;
//        break;
//    case "Botev Vratsa":
//        homeTeamId = 18;
//        break;
//    case "Hebar":
//        homeTeamId = 19;
//        break;
//    case "Pirin Blagoevgrad":
//        homeTeamId = 20;
//        break;
//    case "Spartak Varna":
//        homeTeamId = 21;
//        break;
//    default:
//        homeTeamId = 6;
//        break;
//}
//switch (AwayTeam)
//{
//    case "CSKA Sofia":
//        awayTeamId = 6;
//        break;
//    case "Ludogorets":
//        awayTeamId = 7;
//        break;
//    case "CSKA 1948 Sofia":
//        awayTeamId = 8;
//        break;
//    case "Lok. Plovdiv":
//        awayTeamId = 9;
//        break;
//    case "Slavia Sofia":
//        awayTeamId = 10;
//        break;
//    case "Cherno More":
//        awayTeamId = 11;
//        break;
//    case "Levski Sofia":
//        awayTeamId = 12;
//        break;
//    case "Lok. Sofia":
//        awayTeamId = 13;
//        break;
//    case "Arda":
//        awayTeamId = 14;
//        break;
//    case "Botev Plovdiv":
//        awayTeamId = 15;
//        break;
//    case "Beroe":
//        awayTeamId = 16;
//        break;
//    case "Septemvri Sofia":
//        awayTeamId = 17;
//        break;
//    case "Botev Vratsa":
//        awayTeamId = 18;
//        break;
//    case "Hebar":
//        awayTeamId = 19;
//        break;
//    case "Pirin Blagoevgrad":
//        awayTeamId = 20;
//        break;
//    case "Spartak Varna":
//        awayTeamId = 21;
//        break;
//    default:
//        awayTeamId = 6;
//        break;
//}