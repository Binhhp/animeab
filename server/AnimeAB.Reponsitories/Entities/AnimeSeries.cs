using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Entities
{
    public class AnimeSeries
    {
        public string Key { get; set; }
        public int Ordinal { get; set; } = 1;
        public string AnimeTitle { get; set; }
        public int YearProduce { get; set; }
        public string Session { get; set; }
        public string LinkStart { get; set; }
        public string LinkEnd { get; set; }
    }
}
