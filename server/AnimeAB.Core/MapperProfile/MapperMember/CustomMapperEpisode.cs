using AnimeAB.Reponsitories.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AnimeAB.Core.MapperProfile.MapperMember
{
    public static class CustomMapperEpisode
    {
        public static string MapperLink(this AnimeDetailed opt)
        {
            string link = opt.Link;
            if (!string.IsNullOrWhiteSpace(opt.LinkHH247)
                    && opt.LinkHH247.Contains("+"))
            {
                List<string> episodeHH247 = opt.LinkHH247.Split("+").ToList();
                link = episodeHH247.GetPlayerAsync();
            }
            return link;
        }

        public static string MapperType(this AnimeDetailed opt, string server = "")
        {
            if (server.Equals("animevsub"))
            {
                if (opt.Iframe) return "fembed";
                else return "hls";
            }

            if (string.IsNullOrWhiteSpace(opt.LinkHH247) && opt.Iframe) return "fembed";
            else return "hls";
        }
    }
}
