
using AnimeAB.Domain.DTOs;
using System.Threading.Tasks;

namespace AnimeAB.Application.Common.Interface.Reponsitories
{
    public interface IRefreshTokenGuide
    {
        Task<ResponseRefreshToken> RefreshTokenAsync(string refresh_token);
    }
}
