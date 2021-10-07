using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain
{
    public class ClientToken
    {
        public string? localId { get; set; }
        public string? token { get; set; }
        public string? refresh_token { get; set; }
    }
}
