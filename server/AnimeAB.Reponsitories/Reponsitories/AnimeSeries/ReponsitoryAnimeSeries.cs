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
                var data = await database.GetAsync(Table.ANIMESERIES + "/" + series);
                if (data.Body != "null") return false;

                var item = new Series { Key = series, DateCreated = DateTime.Now };
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
        public async Task<bool> DeleteSeriesAsync(string series, List<Animes> animes)
        {
            try
            {
                var list = animes.Where(x => x.Series.Equals(series)).ToList();
                if(list.Count > 0)
                {
                    list.ForEach(item => database.SetAsync(Table.ANIME + "/" + item.Key + "/Series", ""));
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
        public bool CreateAnimeSeriesAsync(string series, string[] idAnimes)
        {
            try
            {
                idAnimes.ToList().ForEach(id => database.SetAsync(Table.ANIME + "/" + id + "/Series", series));

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
        public bool DeleteAnimeSeriesAsync(string idAnime)
        {
            try
            {
                database.SetAsync(Table.ANIME + "/" + idAnime + "/Series", "");
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
