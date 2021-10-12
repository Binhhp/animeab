using AnimeAB.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Application.Reponsitories
{
    public interface IRefreshTokenGuide
    {
        Task<ResponseRefreshToken> RefreshTokenAsync(string refresh_token);
    }
}
