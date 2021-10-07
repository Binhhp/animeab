using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain.DTOs
{
    public class AnimeView
    {
        public DateTime DateCreated { get; set; }
        public int Views { get; set; }
        public int ViewDays { get; set; }
        public int ViewMonths { get; set; }
        public int ViewWeeks { get; set; }
    }
}
