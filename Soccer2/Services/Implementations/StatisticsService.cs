using Soccer2.Data;
using Soccer2.Models;
using Soccer2.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Services.Implementations
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext db;

        public StatisticsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<ServiceTeamModel> TeamByLeague(string league)
        {
            if (league != null)
            {
                var splitLeague = league.Split('-');
                var replaceDashWithSpace = splitLeague[0] + ' ' + splitLeague[1];
                league = replaceDashWithSpace;
            }

            var getTeams = this.db.Teams.Where(x => x.League == league)
                .Select(t => new ServiceTeamModel
                {
                    Name = t.Name,
                    League = t.League,
                }).ToList();

            return getTeams;
        }

        public void TeamByLeagueStatistics(string league)
        {
            if (league != null)
            {
                var splitLeague = league.Split('-');
                var replaceDashWithSpace = splitLeague[0] + ' ' + splitLeague[1];
                league = replaceDashWithSpace;
            }

            var getTeams = this.db
                .Teams
                .Where(x => x.League == league)
                .Select(t => new TeamModel
                {
                    Name = t.Name,
                    League = t.League,
                    Games = t.Games.Where(x => x.AwayTeamName == t.Name && x.HomeTeamName == t.Name),
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
                    }),
                });
            ;
        }

        public ServiceStatisticsModel TeamStatsSortExcersise(string name, int gamesNumber)
        {
            var teamMatches = this.db.Games.Where(x => x.HomeTeamName == name || x.AwayTeamName == name)
                .ToList();

            double avrGoalsPerGame = AverageGoalsPerGame(teamMatches);
            int tottalGoalsLastGames = GoalsLastGames(gamesNumber, teamMatches);

            var goalsStatistics = new ServiceStatisticsModel();
            GamesWithSameResult(gamesNumber,teamMatches, name);
            goalsStatistics.AverageGoalsPerGame = avrGoalsPerGame;
            goalsStatistics.TottalGoalsLastGames = tottalGoalsLastGames;

            return goalsStatistics;
        }

        //last 3 games 
        
        public int GoalsLastGames(int gamesNumber, List<Game> teamMatches)
        {
            var goals = 0;
            if (gamesNumber <= teamMatches.Count)
            {
                for (int i = 0; i < gamesNumber; i++)
                {
                     goals += teamMatches[i].HomeResult + teamMatches[i].AwayResult;
                }
            }

            return goals;
        }

        public double AverageGoalsPerGame(List<Game> teamMatches)
        {
            var tottalGoalsPerGame = 0;
            foreach (var game in teamMatches)
            {
                var goalsPerGame = game.HomeResult + game.AwayResult;
                tottalGoalsPerGame += goalsPerGame;
            }

            double avrGoals = (double)tottalGoalsPerGame / (double)teamMatches.Count;
            return avrGoals;
        }

        public void GamesWithSameResult(int gamesNumber, List<Game> teamMatches,string teamName)
        {
            foreach (var game in teamMatches)
            {
                for (int i = 0; i < gamesNumber; i++)
                {

                }
            }
        }

        public void MoreGames()
        {

        }
    }
}
