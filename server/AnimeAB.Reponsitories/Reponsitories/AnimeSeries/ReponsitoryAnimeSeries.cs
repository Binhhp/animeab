using AnimeAB.Reponsitories.Entities;
using AnimeAB.Reponsitories.Interface;
using Firebase.Storage;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.AnimeSeries
{
    public class ReponsitoryAnimeSeries : IReponsitoryAnimeSeries
    {
        private readonly IFirebaseClient database;

        public ReponsitoryAnimeSeries(AppSettingFirebase appSetting)
        {
            database = FirebaseManager.Database(appSetting.AuthSecret, appSetting.DatabaseURL);
        }
        /// <summary>
        /// Get curent series
        /// </summary>
        /// <param name="series"></param>
        /// <returns></returns>
        public async Task<List<Entities.AnimeSeries>> GetCurentSeriesAsync(string series)
        {
            try
            {
                List<Entities.AnimeSeries> animeSeries = new List<Entities.AnimeSeries>();
                var data = await database.GetAsync(Table.ANIMESERIES + "/" + series + "/" + Table.ANIMESERIESDETAIL);
                if(data.Body != "null")
                {
                    animeSeries = (data.ResultAs<Dictionary<string, Entities.AnimeSeries>>()).Values.ToList();
                }
                return animeSeries;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Series
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Series>> GetSeriesAsync()
        {
            try
            {
                var series = new List<Series>();

                var data = await database.GetAsync(Table.ANIMESERIES);
                if (data.Body != "null")
                {
                    var seriesData = data.ResultAs<Dictionary<string, Series>>();
                    series = seriesData.Values.OrderByDescending(x => x.DateCreated).ToList();
                }
                return series;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Create series
        /// </summary>
        /// <param name="series"></param>
        /// <returns></returns>
        public async Task<bool> CreateSeriesAsync(string series)
        {
            try
            {
                var item = new Series { Key = series, DateCreated = DateTime.UtcNow };
                await database.SetAsync(Table.ANIMESERIES + "/" + series, item);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Delete series
        /// </summary>
        /// <param name="series"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSeriesAsync(string series)
        {
            try
            {
                var data = await database.GetAsync(Table.ANIMESERIES + "/" + series + "/" + Table.ANIMESERIESDETAIL);
                if(data.Body != "null")
                {
                    var animeSeries = (data.ResultAs<Dictionary<string, Entities.AnimeSeries>>()).Keys.ToList();
                    await Task.WhenAll(animeSeries.Select(s => Task.Run(() => database.SetAsync(Table.ANIME + "/" + s + "/Series", ""))));
                }
                await database.DeleteAsync(Table.ANIMESERIES + "/" + series);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Create animes series
        /// </summary>
        /// <param name="series"></param>
        /// <param name="animeSeries"></param>
        /// <returns></returns>
        public async Task<bool> CreateAnimeSeriesAsync(string series, Entities.AnimeSeries animeSeries)
        {
            try
            {
                var data = await database.GetAsync(Table.ANIMESERIES + "/" + series + "/" + Table.ANIMESERIESDETAIL + "/" + animeSeries.Key);
                if(data.Body != "null")
                {
                    return false;
                }

                var setAnimeSeries = Task.Run(() => database.SetAsync(Table.ANIMESERIES + "/" + series + "/" + Table.ANIMESERIESDETAIL + "/" + animeSeries.Key, animeSeries));
                var setAnime = Task.Run(() => database.SetAsync(Table.ANIME + "/" + animeSeries.Key + "/Series", series));

                await Task.WhenAll(setAnimeSeries, setAnime);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Delete anime series
        /// </summary>
        /// <param name="series"></param>
        /// <param name="animeSeries"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAnimeSeriesAsync(string series, string animeSeries)
        {
            try
            {
                var removeSeries = Task.Run(() => database.DeleteAsync(Table.ANIMESERIES + "/" + series + "/" + Table.ANIMESERIESDETAIL + "/" + animeSeries));
                var deleteAnimeSeries = Task.Run(() => database.SetAsync(Table.ANIME + "/" + animeSeries + "/Series", ""));
                
                await Task.WhenAll(removeSeries, deleteAnimeSeries);
                
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
