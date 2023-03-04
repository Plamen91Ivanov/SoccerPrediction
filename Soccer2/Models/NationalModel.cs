using Soccer2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Models
{
    public class NationalModel
    {
        public string Name { get; set; }

        public IEnumerable<League> League { get; set; }
    }
}
