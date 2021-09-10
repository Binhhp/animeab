using AnimeAB.Reponsitories.Reponsitories.TokenManager.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.TokenManager
{
    public interface IRefreshTokenGuide
    {
        Task<ResponseRefreshToken> RefreshTokenAsync(string refresh_token);
    }
}
