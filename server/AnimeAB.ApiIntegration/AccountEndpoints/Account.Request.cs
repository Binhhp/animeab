using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.ApiIntegration.AccountEndpoints
{
    public class AccountRequest
    {
        public string email { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
    }
}
