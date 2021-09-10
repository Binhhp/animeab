using AnimeAB.Reponsitories.Reponsitories.TokenManager.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.TokenManager
{
    public class RefreshTokenGuide : IRefreshTokenGuide
    {
        private string endpoint_refresh { get; set; }

        public RefreshTokenGuide(string endpoint_refresh)
        {
            this.endpoint_refresh = endpoint_refresh;
        }

        public async Task<ResponseRefreshToken> RefreshTokenAsync(string refresh_token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var pairs = new Dictionary<string, string>();
                pairs.Add("grant_type", "refresh_token");
                pairs.Add("refresh_token", refresh_token);

                var req = await client.PostAsync(endpoint_refresh, new FormUrlEncodedContent(pairs));
                var res = await req.Content.ReadAsStringAsync();

                ResponseRefreshToken refreshToken = JsonConvert.DeserializeObject<ResponseRefreshToken>(res);
                return refreshToken;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
