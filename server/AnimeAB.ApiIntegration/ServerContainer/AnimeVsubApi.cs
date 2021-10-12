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

                var formData = new Dictionary<string, string>();
                formData.Add("link", episode[0]);
                formData.Add("id", episode[1]);

                if(episode.Count > 2)
                {
                    formData.Add("play", episode[2]);
                    formData.Add("backuplinks", "1");
                }

                var formContent = new FormUrlEncodedContent(formData);

                var result = client.PostAsync(uri, formContent).Result;
                string rep = result.Content.ReadAsStringAsync().Result;

                string linkPlayer = "";
                if(episode[2] == "embed") linkPlayer = FormatResponse<AnimeVsubEmbed>.Convert(rep).link;
                else linkPlayer = FormatResponse<AnimeVsub>.Convert(rep).link.First().file;
                return linkPlayer;
            }
            catch
            {
                return current;
            }
        }
        private class FormatResponse<T>
        {
            public static T Convert(string input)
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
        }
    }
}
