using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Domain
{
    public class AnimeFilterRoot
    {
        public string q { get; set; } = "";
        public string cate { get; set; } = "";
        public string collect { get; set; } = "";
        public int stus { get; set; } = 0;
        public int completed { get; set; } = 0;
        public int trend { get; set; } = 0;
        public string id { get; set; } = "";
        public bool offer { get; set; } = false;
        public string asc { get; set; } = "";
        public string des { get; set; } = "";
        public int take { get; set; } = 0;
        public bool random { get; set; } = false;
        public bool banner { get; set; } = false;
    }
}
