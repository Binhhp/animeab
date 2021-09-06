using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnimeAB.Models
{
    public class ResponseAnimeVsub
    {
        public List<LinkMovies> link { get; set; }
        public string playTech { get; set; }
        public int success { get; set; }
        public string title { get; set; }
        public int _fxStatus { get; set; }
    }

    public class LinkMovies
    {
        public string file { get; set; }
        public string label { get; set; }
        public string preload { get; set; }
        public string type { get; set; }
    }
}
