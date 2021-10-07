using AnimeAB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Application.Common.Interface.Reponsitories
{
    public interface IReponsitoryAnimeSeries
    {
        Task<IEnumerable<Series>> GetSeriesAsync();
        Task<bool> CreateSeriesAsync(string series);
        Task<bool> DeleteSeriesAsync(string series, List<Animes> animes);
        bool CreateAnimeSeriesAsync(string series, string[] idAnimes);
        bool DeleteAnimeSeriesAsync(string idAnime);
    }
}
