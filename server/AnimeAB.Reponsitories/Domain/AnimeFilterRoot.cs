using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Domain
{
    public class AnimeFilterRoot
    {
        public string cate { get; set; } = "";
        public string collect { get; set; } = "";
        public string status { get; set; } = "";
        public bool rank { get; set; } = false;
        public string id { get; set; } = "";
        //sorting
        public string sort { get; set; } = "";
        public string sort_by { get; set; } = "";
        public string order { get; set; } = "";
        public int take { get; set; } = 0;
        public int random { get; set; } = 0;
        public bool banner { get; set; } = false;
        public string type { get; set; } = "";

        //filter
        public string q { get; set; }
    }
}
