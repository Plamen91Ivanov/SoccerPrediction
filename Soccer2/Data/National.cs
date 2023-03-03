using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Data
{
    public class National
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<League> Leagues { get; set; }
    }
}
