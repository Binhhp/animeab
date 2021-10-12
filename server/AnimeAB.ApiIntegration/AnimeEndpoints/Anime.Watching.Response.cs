using System;

namespace AnimeAB.ApiIntegration.AnimeEndpoints
{
    public class AnimeWatchingResponse
    {
        public string Key { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Link {  get; set; }
        public int EpisodeMoment { get; set; }
        public int Episodes { get; set; }
        public DateTime Date { get; set; }
    }
}
