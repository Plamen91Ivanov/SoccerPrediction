using Soccer2.Data;
using Soccer2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext db;

        public TeamService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<TeamModel> First10()
          => this.db
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
               }).Take(10);

        public TeamModel ById()
        {
            throw new NotImplementedException();
        }
    }
}
