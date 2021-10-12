using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace AnimeAB.ApiIntegration.ServerContainer
{
    public static class VuigheApi
    {
        public static string type { get; set; }
        public static string GetPlayerVuighe(this List<string> episode)
        {
            try
            {
                HttpClient client = new HttpClient();
                string idAnime = episode[0];
                string idEpisode = episode[1];

                string url = "https://vuighe.net/api/v2/films/" + idAnime + "/episodes/" + idEpisode;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                client.DefaultRequestHeaders.Add("Referer", "https://vuighe.net/idoly-pride");

                var result = client.GetAsync(url).Result;
                string rep = result.Content.ReadAsStringAsync().Result;

                VuigheResponse response = JsonConvert.DeserializeObject<VuigheResponse>(rep);
                if(response.sources.fb.Count > 0)
                {
                    type = "hls";
                    return response.sources.fb.First().src;
                }
            
                if(response.sources.vip.Count > 0)
                {
                    type = "video/mp4";
                    return response.sources.vip.First().src;
                }

                if (response.sources.gd.Count > 0)
                {
                    type = "hls";
                    return response.sources.gd.First().src;
                }

                if (response.sources.pt.Count > 0)
                {
                    type = "hls";
                    return response.sources.pt.First().src;
                }

                if (response.sources.yt.Count > 0)
                {
                    type = "hls";
                    return response.sources.yt.First().src;
                }

                if (response.sources.mp4 != "null" && !string.IsNullOrWhiteSpace(response.sources.mp4))
                {
                    type = "mp4";
                    return response.sources.mp4;
                }

                if (response.sources.embed != "null" && !string.IsNullOrWhiteSpace(response.sources.embed))
                {
                    type = "fembed";
                    string link = response.sources.embed;
                    if(link.Contains("mephimanh.com"))
                    {
                        link = link.Replace("mephimanh.com", "ima21.xyz");
                    }

                    return link;
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
