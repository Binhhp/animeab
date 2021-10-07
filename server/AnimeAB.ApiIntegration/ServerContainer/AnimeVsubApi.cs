
using AnimeAB.ApiIntegration.ServerContainer.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace AnimeAB.ApiIntegration.ServerContainer
{
    public static class AnimeVsubApi
    {
        public static string GetPlayerVsub(this List<string> episode, string current)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri("http://animevietsub.tv/ajax/player?v=2019a");

                var formData = new[]
                {
                    new KeyValuePair<string, string>("link", episode[0]),
                    new KeyValuePair<string, string>("id", episode[1])
                };

                if(episode.Count > 2)
                {
                    formData.Append(new KeyValuePair<string, string>("play", episode[2]));
                    formData.Append(new KeyValuePair<string, string>("backuplinks", "1"));
                }

                var formContent = new FormUrlEncodedContent(formData);

                var result = client.PostAsync(uri, formContent).Result;
                string rep = result.Content.ReadAsStringAsync().Result;
                
                var data = JsonConvert.DeserializeObject<AnimeVsub>(rep);

                string linkPlayer = data.link.First().file;
                return linkPlayer;
            }
            catch
            {
                return current;
            }
        }
    }
}
