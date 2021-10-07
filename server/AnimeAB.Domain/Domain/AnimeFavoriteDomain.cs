using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain
{
    public class AnimeFavoriteDomain
    {
        public string? UserUid { get; set; }
        public string? Key { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? TitleVie { get; set; }
        public int Episode { get; set; }
        public int EpisodeMoment { get; set; }
        public int Views { get; set; }
        public string? Type { get; set; }
        public string? LinkStart { get; set; }
        public string? LinkEnd { get; set; }
        public int IsStatus { get; set; }
        public int MovieDuration { get; set; }
    }
}
