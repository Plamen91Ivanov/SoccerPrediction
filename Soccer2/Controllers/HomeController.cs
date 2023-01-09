using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Soccer2.Data;
using Soccer2.Models;
using Soccer2.Services;
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

        private readonly ITeamService teamService;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, ITeamService teamService)
        {
            _db = db;
            _logger = logger;
            this.teamService = teamService;
        }

        public IActionResult Index()
        {
            //IEnumerable<GameInfo> Games = _db.GamesInfo.Where(x => x.AwayTeam == " Spartak Varna" || x.HomeTeam == "Spartak Varna");
            //IEnumerable<GameInfo> Games = _db.GamesInfo.AsEnumerable().OrderBy(c => c.Date);
            //IEnumerable<Game> Games = _db.Games.AsEnumerable().OrderBy(c => c.Date);
            //IEnumerable<TeamModel> Games = _db
            //     .Teams
            //     .Select(t => new TeamModel
            //     {
            //         Name = t.Name,
            //         AwayGames = t.AwayGames.Select(g => new Game
            //         {
            //             HomeResult = g.HomeResult,
            //             AwayResult = g.AwayResult,
            //             Date = g.Date,
            //             Winner = g.Winner,
            //             AwayCoef = g.AwayCoef,
            //             DrawCoef = g.DrawCoef,
            //             HomeCoef = g.HomeCoef,
            //             HomeTeamName = g.HomeTeamName,
            //             AwayTeamName = g.AwayTeamName

            //         }),
            //         HomeGames = t.HomeGames.Select(g => new Game
            //         {
            //             HomeResult = g.HomeResult,
            //             AwayResult = g.AwayResult,
            //             Date = g.Date,
            //             Winner = g.Winner,
            //             AwayCoef = g.AwayCoef,
            //             DrawCoef = g.DrawCoef,
            //             HomeCoef = g.HomeCoef,
            //             HomeTeamName = g.HomeTeamName,
            //             AwayTeamName = g.AwayTeamName
            //         })
            //     }).Take(10); 

            var Games = this.teamService.First10();

            List<TeamModel> TeamsWithSortedGames = new List<TeamModel>();

            foreach (var team in Games)
            {
                var teamModel = new TeamModel
                {
                    Name = team.Name
                };
                teamModel.Matches = new List<Game>();

                foreach (var homeMatch in team.HomeGames)
                {
                    teamModel.Matches.Add(homeMatch);
                }
                foreach (var awayMatch in team.AwayGames)
                {
                    teamModel.Matches.Add(awayMatch);
                }
                teamModel.Matches = teamModel.Matches.OrderBy(x => x.Date).ToList();

                TeamsWithSortedGames.Add(teamModel);
            }

            var finished = new List<TeamModel>();

            //try to make this more abstract 

            //get last matches with same result !
            foreach (var team in TeamsWithSortedGames)
            {
                var name = team.Name;
                var lastMatches = team.Matches.TakeLast(3).ToArray();
                if (lastMatches.Count() > 2)
                {
                    if (lastMatches[0].Winner == lastMatches[1].Winner 
                        && lastMatches[0].Winner == lastMatches[2].Winner)
                    {
                    var TeamWithSameResultLastMatches = new TeamModel
                    {
                        Name = name,
                        Matches = team.Matches.TakeLast(3).ToList(),
                    };
                        finished.Add(TeamWithSameResultLastMatches);
                    }
                }
            }

            var TeamsAverageProbability = new List<TeamProbability>();

            foreach (var team in TeamsWithSortedGames)
            {
                var name = team.Name;
                var currentProb = 0.0;
                var highestProb = 0.0;
                List<double> totalProb = new List<double>();
               
                var LastResult = "";

                foreach (var match in team.Matches)
                {
                    if (match.HomeTeamName == name)
                    {
                        var homeCoefProbability = (1 / match.HomeCoef) * 100;

                        //add all probabilities to list
                        totalProb.Add(homeCoefProbability);

                        //get curent and highest probability
                        if (LastResult == "")
                        {
                            currentProb += homeCoefProbability;

                            if (currentProb > highestProb)
                            {
                                highestProb = currentProb;
                            }
                        }
                        else if(LastResult == match.Winner)
                        {
                            currentProb += homeCoefProbability;
                            if (currentProb > highestProb)
                            {
                                highestProb = currentProb;
                            }
                        }
                        else
                        {
                            currentProb = homeCoefProbability;
                        }

                        if (match.Winner == name)
                        {
                            LastResult = name;
                        }
                        else if (match.Winner == "x")
                        {
                            LastResult = "x";
                        }
                        else
                        {
                            //LastResult = ;
                        }
                    }
                    else
                    {
                        var awayCoefProbability = (1 / match.AwayCoef) * 100;

                        totalProb.Add(awayCoefProbability);

                        if (LastResult == "")
                        {
                            currentProb += awayCoefProbability;

                            if (currentProb > highestProb)
                            {
                                highestProb = currentProb;
                            }
                        }
                        else if (LastResult == match.Winner)
                        {
                            currentProb += awayCoefProbability;
                            if (currentProb > highestProb)
                            {
                                highestProb = currentProb;
                            }
                        }
                        else
                        {
                            currentProb = awayCoefProbability;
                        }

                        if (match.Winner == name)
                        {
                            LastResult = "win";
                        }
                        else if (match.Winner == "x")
                        {
                            LastResult = "x";
                        }
                        else
                        {
                            LastResult = "loose";
                        }
                    }
                }

                var averageProb = totalProb.Average();
                var currentTeamProbability = new TeamProbability()
                {
                    Name = team.Name,
                    AverageProbability = averageProb,
                    HighestProbability = highestProb,
                    CurrentProbability = currentProb,
                };

                TeamsAverageProbability.Add(currentTeamProbability);
            }

            return View(finished);
        }

        public IActionResult TeamsInformation(string league, string sort)
        {
            IEnumerable<TeamModel> Games;
            if (league != null)
            {
                Games = _db
                .Teams
                .Where(x => x.League == league)
                .Select(t => new TeamModel
                {
                    Name = t.Name,
                    AwayGames = t.AwayGames.Select(g => new Game
                    {
                        HomeResult = g.HomeResult,
                        AwayResult = g.AwayResult,
                        Date = g.Date,
                        Winner = g.Winner,
                        AwayCoef = g.AwayCoef,
                        DrawCoef = g.DrawCoef,
                        HomeCoef = g.HomeCoef,
                        HomeTeamName = g.HomeTeamName,
                        AwayTeamName = g.AwayTeamName
                    }),
                    HomeGames = t.HomeGames.Select(g => new Game
                    {
                        HomeResult = g.HomeResult,
                        AwayResult = g.AwayResult,
                        Date = g.Date,
                        Winner = g.Winner,
                        AwayCoef = g.AwayCoef,
                        DrawCoef = g.DrawCoef,
                        HomeCoef = g.HomeCoef,
                        HomeTeamName = g.HomeTeamName,
                        AwayTeamName = g.AwayTeamName
                    })
                });
            }
            else
            {
                 Games = _db
                .Teams
                .Select(t => new TeamModel
                {
                    Name = t.Name,
                    AwayGames = t.AwayGames.Select(g => new Game
                    {
                        HomeResult = g.HomeResult,
                        AwayResult = g.AwayResult,
                        Date = g.Date,
                        Winner = g.Winner,
                        AwayCoef = g.AwayCoef,
                        DrawCoef = g.DrawCoef,
                        HomeCoef = g.HomeCoef,
                        HomeTeamName = g.HomeTeamName,
                        AwayTeamName = g.AwayTeamName
                    }),
                    HomeGames = t.HomeGames.Select(g => new Game
                    {
                        HomeResult = g.HomeResult,
                        AwayResult = g.AwayResult,
                        Date = g.Date,
                        Winner = g.Winner,
                        AwayCoef = g.AwayCoef,
                        DrawCoef = g.DrawCoef,
                        HomeCoef = g.HomeCoef,
                        HomeTeamName = g.HomeTeamName,
                        AwayTeamName = g.AwayTeamName
                    })
                }).Take(1);
            }

            List<TeamModel> TeamsWithSortedGames = new List<TeamModel>();

            foreach (var team in Games)
            {
                var teamModel = new TeamModel
                {
                    Name = team.Name
                };
                teamModel.Matches = new List<Game>();

                foreach (var homeMatch in team.HomeGames)
                {
                    teamModel.Matches.Add(homeMatch);
                }
                foreach (var awayMatch in team.AwayGames)
                {
                    teamModel.Matches.Add(awayMatch);
                }
                teamModel.Matches = teamModel.Matches.OrderBy(x => x.Date).ToList();

                TeamsWithSortedGames.Add(teamModel);
            }

            var finished = new List<TeamModel>();

            //try to make this more abstract 

            //get last matches with same result !
            foreach (var team in TeamsWithSortedGames)
            {
                var name = team.Name;
                var lastMatches = team.Matches.TakeLast(3).ToArray();
                if (lastMatches.Count() > 2)
                {
                    if (lastMatches[0].Winner == lastMatches[1].Winner
                        && lastMatches[0].Winner == lastMatches[2].Winner)
                    {
                        var TeamWithSameResultLastMatches = new TeamModel
                        {
                            Name = name,
                            Matches = team.Matches.TakeLast(3).ToList(),
                        };
                        finished.Add(TeamWithSameResultLastMatches);
                    }
                }
            }

            var TeamsAverageProbability = new List<TeamProbability>();

            foreach (var team in TeamsWithSortedGames)
            {
                var name = team.Name;
                var currentProb = 0.0;
                var highestProb = 0.0;
                List<double> totalProb = new List<double>();

                var LastResult = "";

                foreach (var match in team.Matches)
                {
                    if (match.HomeTeamName == name)
                    {
                        var dateTimeFormat = match.Date.ToString("yyyy-MM-dd");

                        var homeCoefProbability = Math.Round(((1 / match.HomeCoef) * 100), 3);

                        //add all probabilities to list
                        totalProb.Add(homeCoefProbability);

                        //get curent and highest probability
                        if (LastResult == "")
                        {
                            Math.Round((currentProb += homeCoefProbability), 2);

                            if (currentProb > highestProb)
                            {
                                highestProb = currentProb;
                            }
                        }
                        else if (LastResult == match.Winner)
                        {
                            currentProb += homeCoefProbability;
                            if (currentProb > highestProb)
                            {
                                highestProb = currentProb;
                            }
                        }
                        else
                        {
                            Math.Round((currentProb = homeCoefProbability), 2);
                        }

                        //idc
                        if (match.Winner == name)
                        {
                            LastResult = name;
                        }
                        else if (match.Winner == "x")
                        {
                            LastResult = "x";
                        }
                        else
                        {
                            //LastResult = ;
                        }
                    }
                    else
                    {
                        var awayCoefProbability = Math.Round(((1 / match.AwayCoef) * 100), 3);

                        totalProb.Add(awayCoefProbability);

                        if (LastResult == "")
                        {
                            Math.Round((currentProb += awayCoefProbability), 2);

                            if (currentProb > highestProb)
                            {
                                highestProb = currentProb;
                            }
                        }
                        else if (LastResult == match.Winner)
                        {
                            currentProb += awayCoefProbability;
                            if (currentProb > highestProb)
                            {
                                highestProb = currentProb;
                            }
                        }
                        else
                        {
                            Math.Round((currentProb = awayCoefProbability), 2);
                        }

                        if (match.Winner == name)
                        {
                            LastResult = "win";
                        }
                        else if (match.Winner == "x")
                        {
                            LastResult = "x";
                        }
                        else
                        {
                            LastResult = "loose";
                        }
                    }

                    var matchesWithOutWinInARow = 0;
                    var previousResult = "";
                    if (match.Winner != name && previousResult != match.Winner)
                    {
                        matchesWithOutWinInARow++;
                    }
                }

                var averageProb = Math.Round((totalProb.Average()),2);
                var currentTeamProbability = new TeamProbability()
                {
                    Name = team.Name,
                    AverageProbability = averageProb,
                    HighestProbability = highestProb,
                    CurrentProbability = currentProb,
                    TeamGames = team.Matches,
                };
                TeamsAverageProbability.Add(currentTeamProbability);
            }

            switch (sort)
            {
                case "High":
                    TeamsAverageProbability = TeamsAverageProbability.OrderByDescending(x => x.HighestProbability).ToList();
                    break;
                case "Average":
                    TeamsAverageProbability = TeamsAverageProbability.OrderByDescending(x => x.AverageProbability).ToList();
                    break;
                case "Current":
                    TeamsAverageProbability = TeamsAverageProbability.OrderByDescending(x => x.CurrentProbability).ToList();
                    break;
                default:
                    TeamsAverageProbability = TeamsAverageProbability.OrderByDescending(x => x.HighestProbability).ToList();
                    break;
            }
            return View(TeamsAverageProbability);
        }

        private int MathRound(double v)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Index(int times, string result)
        {
            IEnumerable<Game> Games = _db.Games.Where(x => x.Winner == result).Take(times);
            return View(Games);
        }

        public IActionResult Test()
        {
            var model = new Test
            {
                Id = 5,
                Name = "tst"
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            //redirect to action
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
