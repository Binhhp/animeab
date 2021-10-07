using AnimeAB.ApiIntegration.ServerContainer;
using AnimeAB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeAB.Core.MapperProfile.MapperMember
{
    public static class CustomMapperEpisode
    {
        //Mapper link vuighe
        public static string MapperVuighe(this AnimeDetailed opt)
        {
            try
            {
                if (opt.LinkVuighe.Contains("+"))
                {
                    List<string> episodeVuighe = opt.LinkVuighe.Split("+").ToList();
                    return episodeVuighe.GetPlayerVuighe();
                }
            }
            catch
            {
                return opt.LinkVuighe;
            }

            return opt.LinkVuighe;
        }

        public static string MapperVuigheType(this AnimeDetailed opt)
        {
            return VuigheApi.type;
        }

        //Mapper auto link animevietsub + hoathinh247
        public static string MapperLink(this AnimeDetailed opt, bool isAnimeVsub = false)
        {
            string link = opt.Link;
            if (!string.IsNullOrWhiteSpace(opt.LinkHH247)
                    && opt.LinkHH247.Contains("+") && !isAnimeVsub)
            {
                List<string> episodeHH247 = opt.LinkHH247.Split("+").ToList();
                try
                {
                    //get link video hoathinh247
                    link = episodeHH247.GetPlayerAsync();
                }
                catch
                {
                    //exception get link video animevietsub
                    if (opt.Link.Contains("+"))
                    {
                        List<string> episodeAnimeVsub = opt.Link.Split("+").ToList();
                        link = episodeAnimeVsub.GetPlayerVsub(opt.LinkVuighe);
                    }
                    return link;
                }
            }
            //get link animevietsub
            if (opt.Link.Contains("+"))
            {
                List<string> episodeAnimeVsub = opt.Link.Split("+").ToList();
                link = episodeAnimeVsub.GetPlayerVsub(opt.LinkVuighe);
            }
            return link;
        }
        //Mapper type video
        public static string MapperType(this AnimeDetailed opt, string server = "")
        {
            try
            {
                if (server.Equals("animevsub"))
                {
                    if (opt.Iframe) return "fembed";
                    else return "hls";
                }

                if (string.IsNullOrWhiteSpace(opt.LinkHH247) && opt.Iframe) return "fembed";
                else return "hls";
            }
            catch
            {
                return "hls";
            }
        }
    }
}
