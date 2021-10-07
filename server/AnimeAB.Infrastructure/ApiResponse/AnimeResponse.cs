using AnimeAB.Domain.Entities;
using System;
using System.Collections.Generic;

namespace AnimeAB.Infrastructure.ApiResponse
{
    public class AnimeResponse
    {
        public string Key { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string TitleVie { get; set; }
        public string Description { get; set; }
        public string Trainer { get; set; }
        public string Banner { get; set; } = "";
        //Duration of movie
        public int MovieDuration { get; set; }
        //Date release
        public DateTime DateRelease { get; set; } = DateTime.Now;
        public int Episode { get; set; }
        public int EpisodeMoment { get; set; } = 0;
        public string Link { get; set; }
        //1 Sắp công chiếu 2. Đang chiếu 3. Đã hoàn thành
        public int IsStatus { get; set; }

        public string Collection { get; set; }
        public Dictionary<string, Categories> Categories { get; set; }
        public int Views { get; set; }
        //Series
        public string Series { get; set; } = "";
        public string Type { get; set; }
    }
}
