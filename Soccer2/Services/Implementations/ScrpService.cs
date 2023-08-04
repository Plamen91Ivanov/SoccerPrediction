using HtmlAgilityPack;
using Soccer2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer2.Services.Implementations
{
    public class ScrpService : IScrpService
    {
        private readonly ApplicationDbContext db;

        public ScrpService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void ScrpFutureMatches()
        {
            Console.OutputEncoding = Encoding.UTF8;

            HtmlWeb web = new HtmlWeb();

            var country = "england";
            var league = "premier-league";
            HtmlDocument doc = web.Load($"https://www.betexplorer.com/soccer/{country}/{league}/results/");
            var HeaderNames = doc.DocumentNode.SelectNodes("//tr");

            //get last match date 
            //var getLastMatch = this.db.Games.Where(x => x.League == league).OrderByDescending(x => x.Date).FirstOrDefault();

            DateTime lastMatchDate = new DateTime();

            for (int i = 1; i < HeaderNames.Count - 1; i++)
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
                        else if (HomeResult < AwayResult)
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
                        else if (DateSplit == "Today")
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

            }
        }
    }
}
