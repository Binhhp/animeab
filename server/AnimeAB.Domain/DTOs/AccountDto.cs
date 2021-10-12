using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain.DTOs
{
    public class AccountDto
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string FirebaseToken { get; set; }
        public string Role { get; set; }
    }
}
