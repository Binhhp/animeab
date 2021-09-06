using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnimeAB.Models
{
    public class PayloadAnimeVsub
    {
        public string link { get; set; }
        public string play { get; set; }
        public string id { get; set; }
        public string backuplinks { get; set; } = "1";
    }
}
