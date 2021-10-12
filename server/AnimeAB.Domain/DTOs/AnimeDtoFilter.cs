using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain.DTOs
{
    public class AnimeDtoFilter
    {
        public string Category { get; set; }
        public string Collection { get; set; }
        public int Status { get; set; }
        public int Time { get; set; }
    }
}
