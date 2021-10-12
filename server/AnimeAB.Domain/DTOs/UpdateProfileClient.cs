using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain.DTOs
{
    public class ProfileClient
    {
        public string name { get; set; }
        public string email { get; set; }
        public string photo_url { get; set; }
        public string password { get; set; }
        public string newPassword { get; set; }
    }
}
