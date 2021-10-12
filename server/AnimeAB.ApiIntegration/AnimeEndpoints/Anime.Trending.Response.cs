using System.Collections.Generic;

namespace AnimeAB.ApiIntegration.AnimeEndpoints
{
    public class AnimeTrending
    {
        public IEnumerable<AnimeRankDayResponse> days { get; set; }
        public IEnumerable<AnimeRankWeekResponse> weeks { get; set; }
        public IEnumerable<AnimeRankMonthResponse> months { get; set; }
    }
}
