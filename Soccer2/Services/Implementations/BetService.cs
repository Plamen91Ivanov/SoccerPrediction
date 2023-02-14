using Soccer2.Data;
using Soccer2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Services.Implementations
{
    public class BetService : IBetService
    {
        private readonly ApplicationDbContext db;

        public BetService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<BetInfo> All()
        {
            var allBets = this.db.BetInfo.ToList();
            return allBets;
        }
    }
}
