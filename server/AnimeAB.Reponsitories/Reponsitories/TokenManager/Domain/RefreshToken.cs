﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.TokenManager.Domain
{
    public class RefreshToken
    {
        public string id_token { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
    }
}
