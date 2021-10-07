using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain.DTOs
{
    public class AnimeWatching
    {
        public string? idAnime { get; set; }
        public string? episode {  get; set; }
        public string? link { get; set; }
    }

    public class AnimeWatchingRequest
    {
        public string[]? watching { get; set; }
        public int take { get; set; } = 0;
    }
}
