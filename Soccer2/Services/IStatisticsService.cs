using Soccer2.Data;
using Soccer2.Models;
using Soccer2.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Services
{
    public interface IStatisticsService
    {
        IEnumerable<ServiceTeamModel> TeamByLeague(string league);

        void TeamByLeagueStatistics(string league);

        ServiceStatisticsModel TeamStatsSortExcersise(string name);
    }
}
