using Soccer2.Models;
using Soccer2.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Services
{
    public interface ITeamService
    {
        IEnumerable<TeamModel> First10();

        TeamModel ById();
    }
}
