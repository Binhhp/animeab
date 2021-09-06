using AnimeAB.Reponsitories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.AnimeSeries
{
    public interface IReponsitoryAnimeSeries
    {
        Task<List<Entities.AnimeSeries>> GetCurentSeriesAsync(string series);
        Task<IEnumerable<Series>> GetSeriesAsync();
        Task<bool> CreateSeriesAsync(string series);
        Task<bool> DeleteSeriesAsync(string series);
        Task<bool> CreateAnimeSeriesAsync(string series, Entities.AnimeSeries animeSeries);
        Task<bool> DeleteAnimeSeriesAsync(string series, string animeSeries);
    }
}
