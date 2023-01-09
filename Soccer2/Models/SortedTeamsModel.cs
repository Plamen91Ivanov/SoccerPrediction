using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Models
{
    public class SortedTeamsModel
    {
        public string Name { get; set; }

        public int HomeRes { get; set; }

        public int AwayRes { get; set; }

        public string  Winner { get; set; }
    }
}
