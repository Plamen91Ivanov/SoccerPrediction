using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2.Data
{
    public class League
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NationalId { get; set; }

        public virtual National National { get; set; }
    }
}
