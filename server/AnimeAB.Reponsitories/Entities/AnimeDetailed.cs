using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Entities
{
    public class AnimeDetailed
    {
        public string Key { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; } = "";
        public int Views { get; set; }
        //link animevsub
        public string Link { get; set; }
        public bool Iframe { get; set; } = false;
        //Link server hoat hinh 247
        public string LinkHH247 { get; set; } = "";
        //Link server vuighe
        public string LinkVuighe { get; set; } = "https://video-hkt1-1.xx.fbcdn.net/v/t42.1790-2/10000000_234766821478818_6594318953678058615_n.mp4?_nc_cat=101&ccb=1-5&_nc_sid=985c63&efg=eyJybHIiOjMwMCwicmxhIjo0MDI2LCJ2ZW5jb2RlX3RhZyI6InN2ZV9zZCJ9&_nc_ohc=kNWZ501hwMkAX90dopT&rl=300&vabr=150&_nc_ht=video-hkt1-1.xx&edm=APRAPSkEAAAA&oh=2e1dfaa379def55d1c0a5b44a14f0d18&oe=6120FDF3";
        public int Episode { get; set; }
    }
}
