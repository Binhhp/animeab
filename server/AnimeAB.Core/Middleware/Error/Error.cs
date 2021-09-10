using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.Middleware.Error
{
    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }
}
