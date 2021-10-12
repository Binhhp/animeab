using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain.DTOs
{
    public class CommentRequest
    {
        public string key { get; set; }
        public string user_local { get; set; }
        public string name { get; set; }
        public string photo_url { get; set; }
        public string message { get; set; }
        public DateTime when { get; set; } = DateTime.Now;
        public string reply_comment { get; set; } = "";
    }
}
