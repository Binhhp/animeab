using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnimeAB.Core
{
    public static class HoatHinhApi
    {
        public static string GetPlayerAsync(this List<string> episode)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri("https://hoathinh247tv.com/ajax/5AB412E5033F55E");
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id", episode[0]),
                    new KeyValuePair<string, string>("ep", episode[1])
                });

                var result = client.PostAsync(uri, formContent).Result;
                string rep = result.Content.ReadAsStringAsync().Result;
                Regex regx = new Regex("https://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;]*)?",
                    RegexOptions.IgnoreCase);
                MatchCollection match = regx.Matches(rep);

                string linkPlayer = match[0].Value.ToString();
                linkPlayer = linkPlayer.Replace("306084399", "167335343");
                return linkPlayer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
