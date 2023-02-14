using Soccer2.Data;
using Soccer2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Services
{
    public interface IBetService
    {
        IEnumerable<BetInfo> All();
    }
}
