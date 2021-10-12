using AnimeAB.AppAdmin.AnimeEndpoints;
using AnimeAB.Core.Controllers;

namespace AnimeAB.AppAdmin.MapperProfile.Hook
{
    public static class CustomMapperAnime
    {
        public static string MapperCollect(this AnimeRequest animes)
        {
            if(animes.DateRelease.Month < 4) return "xuan";
            if (animes.DateRelease.Month < 7 && animes.DateRelease.Month > 3) return "he";
            if (animes.DateRelease.Month < 10 && animes.DateRelease.Month > 6) return "thu";

            return "dong";
        }
    }
}
