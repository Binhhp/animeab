using System;

namespace AnimeAB.Core.ApiResponse
{
    public class CommentResponse
    {
        public string Key { get; set; }
        public string UserLocal { get; set; }
        public string DisplayName { get; set; }
        public string PhotoUrl { get; set; }
        public string Message { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
        public string ReplyComment { get; set; } = "";
        public int Likes { get; set; } = 0;
    }
}
