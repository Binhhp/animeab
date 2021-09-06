using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Entities
{
    public class Animes
    {
        public string Key { get; set; }
        public string Image { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string TitleVie { get; set; }
        public string Description { get; set; }
        public string Trainer { get; set; }
        //Duration of movie
        public int MovieDuration { get; set; }
        //Date release
        public DateTime DateRelease { get; set; }

        public int Episode { get; set; }
        public int EpisodeMoment { get; set; } = 0;
        public string LinkStart { get; set; } = "";
        public string LinkEnd { get; set; } = "";
        //1 Sắp công chiếu 2. Đang chiếu 3. Đã hoàn thành
        public int IsStatus { get; set; }
        //Banner image 1300x600
        public string Banner { get; set; } = "";
        public string FileNameBanner { get; set; } = "";
        public bool IsBanner { get; set; }

        public string CollectionId { get; set; }
        public string CategoryKey { get; set; }

        //Curent date update view
        public DateTime DateCreated { get; set; }
        public int Views { get; set; }
        public int ViewDays{ get; set; }
        public int ViewMonths{ get; set; }
        public int ViewWeeks{ get; set; }

        //Series
        public string Series { get; set; } = "";

        public string Type { get; set; }
    }
}
