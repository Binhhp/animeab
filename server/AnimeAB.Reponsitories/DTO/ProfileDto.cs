using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.DTO
{
    public class ProfileDto
    {
        public string LocalId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string PhotoUrl { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
