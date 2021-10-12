
using AnimeAB.Application.Reponsitories;
using AnimeAB.Domain.Entities;
using AnimeAB.Domain.Services;
using AnimeAB.Domain.Settings;
using AnimeAB.Domain.ValueObjects;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Infrastructure.Persistence.Reponsitories
{
    public class ReponsitoryFavorite : IReponsitoryFavorite
    {
        private readonly IFirebaseClient database;

        public ReponsitoryFavorite(AppSettingFirebase _appSetting)
        {
            database = FirebaseManager.Database(_appSetting.AuthSecret, _appSetting.DatabaseURL);
        }
        /// <summary>
        /// Get animes favorite
        /// </summary>
        /// <param name="uid">user uid</param>
        /// <param name="idAnime">id anime</param>
        /// <returns></returns>
        public async Task<IEnumerable<Animes>> GetAnimes(string uid)
        {
            try
            {

                var dataAnime = await database.GetAsync(Table.ANIME);
                var dataFavorite = await database.GetAsync(Table.ANIMEFAVORITE + "/" + uid);

                if (dataAnime.Body == "null" && dataFavorite.Body == "null") return new List<Animes>();

                var favorite = (dataFavorite.ResultAs<Dictionary<string, AnimeFavorite>>()).Values.Select(x => x.AnimeKey).ToList();

                var animes = (dataAnime.ResultAs<Dictionary<string, Animes>>()).Values.ToList();

                animes = animes.Where(x => favorite.Contains(x.Key)).ToList();
                return animes;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Add anime for favorite
        /// </summary>
        /// <param name="animeFavorite">Anime detail</param>
        public void AddAnime(AnimeFavorite animeFavorite)
        {
            try
            {
                var data = database.GetAsync(Table.ANIMEFAVORITE + "/" + animeFavorite.UserUid + "/" + animeFavorite.AnimeKey);
                if (data.Result.Body != "null") throw new Exception("ID_ANIME_VALID");

                database.SetAsync(Table.ANIMEFAVORITE + "/" + animeFavorite.UserUid + "/" + animeFavorite.AnimeKey, animeFavorite);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Remove anime favorite
        /// </summary>
        /// <param name="id">id anime</param>
        /// <param name="uid">uid user logined</param>
        public void RemoveAnime(string idAnime, string uid)
        {
            try
            {
                database.DeleteAsync(Table.ANIMEFAVORITE + "/" + uid + "/" + idAnime);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
