using AnimeAB.Reponsitories.Domain;
using AnimeAB.Reponsitories.Entities;
using AnimeAB.Reponsitories.Interface;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.Favorite
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
        public async Task<IEnumerable<AnimeFavoriteDomain>> GetAnimes(string uid)
        {
            try
            {
                var favorite = new List<AnimeFavoriteDomain>();

                var dataAnime = await database.GetAsync(Table.ANIME);
                var dataFavorite = await database.GetAsync(Table.ANIMEFAVORITE + "/" + uid);

                if (dataAnime.Body == "null" && dataFavorite.Body == "null") return favorite;

                List<Animes> animes = (dataAnime.ResultAs<Dictionary<string, Animes>>()).Values.ToList();
                List<AnimeFavorite> favoriteUsers =
                    (dataFavorite.ResultAs<Dictionary<string, AnimeFavorite>>()).Values.ToList();

                List<AnimeFavoriteDomain> result = favoriteUsers.Join(animes,
                    fav => fav.AnimeKey,
                    anime => anime.Key,
                    (fav, anime) => new AnimeFavoriteDomain()
                    {
                        Key = anime.Key,
                        Episode = anime.Episode,
                        EpisodeMoment = anime.EpisodeMoment,
                        Image = anime.Image,
                        Title = anime.Title,
                        TitleVie = anime.TitleVie,
                        Type = anime.Type,
                        UserUid = fav.UserUid,
                        Views = anime.Views,
                        LinkEnd = anime.LinkEnd,
                        LinkStart = anime.LinkStart,
                        IsStatus = anime.IsStatus,
                        MovieDuration = anime.MovieDuration,
                    }).ToList();

                return result;

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
