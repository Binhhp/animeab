using AnimeAB.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain.Entities
{
    public class AnimeDetailed
    {
        public string? Key { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? FileName { get; set; } = "";
        public int Views { get; set; }
        //link animevsub
        public string? Link { get; set; }
        public bool Iframe { get; set; } = false;
        public string? LinkHDX { get; set; }
        //Link server vuighe
        public string? LinkVuighe { get; set; } = DefaultEpisode.Video;
        public int Episode { get; set; }
    }
}
