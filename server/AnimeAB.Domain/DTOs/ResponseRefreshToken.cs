using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain.DTOs
{
   public class ResponseRefreshToken
    {
        public string id_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string user_id { get; set; }
    }
}
