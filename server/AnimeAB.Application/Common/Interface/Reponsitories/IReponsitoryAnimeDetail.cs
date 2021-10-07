using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeAB.Application.Common.Behaviour;
using AnimeAB.Domain.Entities;

namespace AnimeAB.Application.Common.Interface.Reponsitories
{
    public interface IReponsitoryAnimeDetail
    {
        Task<bool> UpdateViewAsync(string animeKey, string animeDetailedKey);
        Task<IEnumerable<AnimeDetailed>> GetCurrentAnimeAsync(string key);
        Task<AnimeDetailed> GetAnimeDetailAsync(string animeKey, string episode);
        Task<Response> CreateMovieAsync(AnimeDetailed movie, Stream file, string animeKey);
        Task<Response> UpdateMovieAsync(AnimeDetailed movie, Stream image, string animeKey);
        Task<Response> DeleteMovieAsync(string animeKey, string animeDetailedKey);
    }
}
