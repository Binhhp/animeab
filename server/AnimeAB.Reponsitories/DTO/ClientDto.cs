using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.DTO
{
    public class ClientDto
    {
        public string email { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
    }
}
