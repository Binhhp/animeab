using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain
{
    public class Client
    {
        public string LocalId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }

        public string PhotoUrl { get; set; }
    }
}
