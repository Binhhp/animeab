using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Domain
{
   public class ProfileDomain
    {
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public Stream StreamAvatar { get; set; }
        public string FileName = "";
        public string Email = "";
        public string Avatar = "https://i.imgur.com/t6mJtNE.png";
    }
}
