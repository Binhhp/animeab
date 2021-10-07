namespace AnimeAB.Infrastructure.ApiResponse
{
    public class FavoriteResponse
    {
        public string Key { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        //Duration of movie
        public int MovieDuration { get; set; }
        public int Episode { get; set; }
        public int EpisodeMoment { get; set; } = 0;
        public string Link { get; set; }
        public int Views { get; set; }
        public string Type { get; set; }
    }
}
