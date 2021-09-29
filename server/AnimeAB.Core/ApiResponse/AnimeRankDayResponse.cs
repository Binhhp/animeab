namespace AnimeAB.Core.ApiResponse
{
    public class AnimeRankDayResponse
    {
        public string Key { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Views { get; set; }
        public string Link { get; set; }
    }

    public class AnimeRankWeekResponse : AnimeRankDayResponse
    {
    }

    public class AnimeRankMonthResponse : AnimeRankDayResponse
    {
    }
}
