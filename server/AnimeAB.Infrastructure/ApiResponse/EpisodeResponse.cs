namespace AnimeAB.Infrastructure.ApiResponse
{
    public class EpisodeResponse : EpisodeInstance
    {
        public string Link { get; set; }
        public string Type { get; set; }
    }

    public class EpisodeInstance 
    {
        public string Key { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Views { get; set; }
        public int Episode { get; set; }
    }
}
