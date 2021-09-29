using System.Collections.Generic;

namespace AnimeAB.Core.ApiResponse
{
    public class AnimeTrending
    {
        public List<AnimeRankDayResponse> days { get; set; }
        public List<AnimeRankWeekResponse> weeks { get; set; }
        public List<AnimeRankMonthResponse> months { get; set; }
    }
}
