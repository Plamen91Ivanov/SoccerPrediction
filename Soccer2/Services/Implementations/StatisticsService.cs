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
            ;
        }
    }
}
