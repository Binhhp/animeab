using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeAB.Reponsitories.Domain;
using AnimeAB.Reponsitories.Entities;

namespace AnimeAB.Reponsitories.Reponsitories.Anime
{
    public interface IReponsitoryAnime
    {
        Task<bool> UpdateViewAsync(string key);
        Task<Animes> GetCurentAnimeAsync(string animeKey);
        Task<List<Animes>> GetAnimesAsync();
        Task<Response> CreateAnimeAsync(Animes anime, Stream file);
        Task<Response> UpdateAnimeAsync(AnimesDomain anime, Stream file);
        Task<Response> UpdateBannerAsync(string key, string fileName, Stream banner);
        Task<Response> DestroyBannerAsync(string key);
        Task<Response> DeleteAnimeAsync(string key);
    }
}
