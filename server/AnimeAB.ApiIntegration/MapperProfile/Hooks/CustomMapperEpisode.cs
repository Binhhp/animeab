
using AnimeAB.ApiIntegration.ServerContainer;
using AnimeAB.Domain.Entities;
using AnimeAB.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeAB.ApiIntegration.MapperProfile
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

        //Mapper auto link animevietsub
        public static string MapperLink(this AnimeDetailed opt)
        {
            string link = opt.Link;
            //get link animevietsub
            if (opt.Link.Contains("+"))
            {
                try
                {
                    List<string> episodeAnimeVsub = opt.Link.Split("+").ToList();
                    link = episodeAnimeVsub.GetPlayerVsub(DefaultEpisode.Video);
                }
                catch
                {
                    //set error video link die
                    link = DefaultEpisode.Video;
                }
            }
            return link;
        }
        //Mapper auto link hdx
        public static string MapperLinkHDX(this AnimeDetailed opt)
        {
            try
            {
                string link = opt.LinkHDX;
                if(string.IsNullOrWhiteSpace(opt.LinkHDX)) return DefaultEpisode.Video;
                if (opt.LinkHDX.Contains("+"))
                {
                    List<string> episodeHDX = opt.LinkHDX.Split("+").ToList();
                    link = episodeHDX.GetPlayerVsub(DefaultEpisode.Video);
                }
                return link;
            }
            catch
            {
                return DefaultEpisode.Video; 
            }
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
                if (server.Equals("hdx") || opt.Iframe)
                {
                    if (string.IsNullOrWhiteSpace(opt.LinkHDX)) return "m3u8";
                    return "fembed";
                }
                if (opt.Iframe) return "fembed";


                return "hls";
            }
            catch
            {
                return "hls";
            }
        }
    }
}
