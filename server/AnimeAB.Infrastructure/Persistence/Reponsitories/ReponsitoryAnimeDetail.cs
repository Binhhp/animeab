using AnimeAB.Application.Common.Behaviour;
using AnimeAB.Application.Common.Interface.Reponsitories;
using AnimeAB.Domain.Entities;
using AnimeAB.Domain.Settings;
using AnimeAB.Domain.ValueObject;
using AnimeAB.Infrastructure.Services;
using Firebase.Storage;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnimeAB.Infrastructure.Persistence.Reponsitories
{

    public class ReponsitoryAnimeDetail : Request, IReponsitoryAnimeDetail
    {
        private readonly AppSettingFirebase _appSetting;
        private readonly IFirebaseClient database;
        private readonly FirebaseStorage storage;

        public ReponsitoryAnimeDetail(AppSettingFirebase appSetting)
        {
            _appSetting = appSetting;
            database = FirebaseManager.Database(_appSetting.AuthSecret, _appSetting.DatabaseURL);
            storage = FirebaseManager.Storage(_appSetting.StorageBucket);
        }
        
        /// <summary>
        /// Update view
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateViewAsync(string animeKey, string animeDetailedKey)
        {
            try
            {
                var data = await database.GetAsync(Table.PATHANIMEDETAILED(animeKey, animeDetailedKey));

                if (data.Body == "null") return false;
                AnimeDetailed anime = data.ResultAs<AnimeDetailed>();

                int viewDetail = anime.Views + 1;
                await database.SetAsync(Table.PATHANIMEDETAILED(animeKey, animeDetailedKey) + "/Views", viewDetail);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// List anime detail
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AnimeDetailed>> GetCurrentAnimeAsync(string key)
        {
            try
            {
                var data = await database.GetAsync(Table.ANIME + "/" + key + "/" + Table.ANIMEDETAIL);

                if (data.Body == "null") return new List<AnimeDetailed>();
                var anime = data.ResultAs<Dictionary<string, AnimeDetailed>>().Values.ToList();

                return anime.OrderBy(x => x.Episode).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get episode current
        /// </summary>
        /// <param name="animeKey"></param>
        /// <param name="episode"></param>
        /// <returns></returns>
        public async Task<AnimeDetailed> GetAnimeDetailAsync(string animeKey, string episode)
        {
            try
            {
                var data = await database.GetAsync(Table.PATHANIMEDETAILED(animeKey, episode));
                if (data.Body == "null") return new AnimeDetailed();
                var episodeCurrent = data.ResultAs<AnimeDetailed>();
                return episodeCurrent;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Create animes
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        public async Task<Response> CreateMovieAsync(AnimeDetailed movie, Stream file, string animeKey)
        {
            try
            {

                var animeData = await database.GetAsync(Table.ANIME + "/" + animeKey);
                Animes anime = animeData.ResultAs<Animes>();

                if(anime.IsStatus == 3) return Error("Anime đã upload đủ số tập"); 

                string pathDatabase = Table.PATHANIMEDETAILED(animeKey, movie.Key);

                var data = await database.GetAsync(pathDatabase);
                if (data.Body != "null") return Error("Anime đã được upload");

                var cancellation = new CancellationTokenSource();

                if (file != null)
                {
                    await storage.Child(Table.ANIMEDETAIL).Child(movie.FileName).PutAsync(file, cancellation.Token);
                    string pathImage = await storage.Child(Table.ANIMEDETAIL).Child(movie.FileName).GetDownloadUrlAsync();
                    movie.Image = pathImage;
                }

                Random r = new Random();
                movie.Views = r.Next(50000, 250000);

                var uploadMovie = Task.Run(() => database.SetAsync(pathDatabase, movie));

                anime.EpisodeMoment += 1;
                if(anime.EpisodeMoment == 1)
                {
                    anime.LinkStart = movie.Key;
                }

                if(anime.Episode == anime.EpisodeMoment)
                {
                    anime.IsStatus = 3;
                }
                else
                {
                    anime.IsStatus = 2;
                }

                anime.LinkEnd = movie.Key;

                var updateInforAnime = Task.Run(() => database.UpdateAsync(Table.ANIME + "/" + anime.Key, anime));
                await Task.WhenAll(uploadMovie, updateInforAnime);

                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// Update anime
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        public async Task<Response> UpdateMovieAsync(AnimeDetailed movie, Stream image, string animeKey)
        {
            try
            {
                
                string pathDatabase = Table.PATHANIMEDETAILED(animeKey, movie.Key);

                var data = await database.GetAsync(pathDatabase);

                if (data.Body == "null") return Error("Bản ghi không tồn tại.");

                AnimeDetailed animeDetail = data.ResultAs<AnimeDetailed>();

                if (image != null)
                {
                    var cancellation = new CancellationTokenSource();

                    if(!string.IsNullOrWhiteSpace(animeDetail.FileName))
                    {
                        var deleteImage = Task.Run(() => storage.Child(Table.ANIMEDETAIL).Child(animeDetail.FileName).DeleteAsync());
                        var uploadImageNew = Task.Run(() => storage.Child(Table.ANIMEDETAIL).Child(movie.FileName).PutAsync(image, cancellation.Token));
                        await Task.WhenAll(deleteImage, uploadImageNew);
                    }
                    else
                    {
                       await storage.Child(Table.ANIMEDETAIL).Child(movie.FileName).PutAsync(image, cancellation.Token);
                    }

                    var pathImage = await storage.Child(Table.ANIMEDETAIL).Child(movie.FileName).GetDownloadUrlAsync();
                    
                    movie.Image = pathImage;

                }
                else
                {
                    if (movie.Image.IndexOf("animeab.appspot.com") > -1)
                    {
                        movie.FileName = animeDetail.FileName;
                    }
                    else
                    {
                        if(!string.IsNullOrWhiteSpace(animeDetail.FileName))
                        {
                            await storage.Child(Table.ANIMEDETAIL).Child(animeDetail.FileName).DeleteAsync();
                        }
                        movie.FileName = "";
                    }
                }

                movie.Views = animeDetail.Views;
                movie.Key = animeDetail.Key;

                await database.UpdateAsync(pathDatabase, movie);

                return Success(movie);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// Delete episode anime 
        /// </summary>
        /// <param name="animeDetailedKey">key episode</param>
        /// <param name="animeKey">key anime parent</param>
        /// <returns></returns>
        public async Task<Response> DeleteMovieAsync(string animeKey, string animeDetailedKey)
        {
            try
            {
                var data = await database.GetAsync(Table.PATHANIMEDETAILED(animeKey, animeDetailedKey));

                if (data.Body == "null") return Error("Bản ghi không tồn tại.");
                AnimeDetailed animeDetailed = data.ResultAs<AnimeDetailed>();

                if (!string.IsNullOrWhiteSpace(animeDetailed.FileName))
                {

                    var deleteImage = Task.Run(() => storage.Child(Table.ANIMEDETAIL).Child(animeDetailed.FileName).DeleteAsync());
                    var deleteDb = Task.Run(() => database.DeleteAsync(Table.PATHANIMEDETAILED(animeKey, animeDetailedKey)));

                    await Task.WhenAll(deleteImage, deleteDb);
                }
                else
                {
                    await database.DeleteAsync(Table.PATHANIMEDETAILED(animeKey, animeDetailedKey));
                }

                var animes = await database.GetAsync(Table.ANIME + "/" + animeKey + "/" + Table.ANIMEDETAIL);

                var dataAnime = await database.GetAsync(Table.ANIME + "/" + animeKey);
                Animes anis = dataAnime.ResultAs<Animes>();
                int episodeMoment = anis.EpisodeMoment - 1;

                if (animes.Body == "null")
                {
                    var update = new UpdateAfterUpdate();

                    await database.UpdateAsync(Table.ANIME + "/" + animeKey, update);
                }
                else
                {
                    List<AnimeDetailed> animeDetails = (animes.ResultAs<Dictionary<string, AnimeDetailed>>()).Values.ToList();
                    int count = animeDetails.Count();


                    int indexLinkEnd = count - 1;
                    var item = new AnimeDetailed();

                    if(indexLinkEnd == 0)
                    {
                        item = animeDetails.First();
                    }
                    else
                    {
                        item = animeDetails.Skip(indexLinkEnd).First();
                    }
                    var updateLinkEnd = Task.Run(() => database.SetAsync(Table.ANIME + "/" + animeKey + "/LinkEnd", item.Key));
                    var updateLinkStart = Task.Run(() => database.SetAsync(Table.ANIME + "/" + animeKey + "/LinkStart", animeDetails.First().Key));
                    var updateEpisodeMoment = Task.Run(() => database.SetAsync(Table.ANIME + "/" + animeKey + "/EpisodeMoment", episodeMoment));
                    await Task.WhenAll(updateLinkEnd, updateLinkStart, updateEpisodeMoment);
                }
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }

    public class UpdateAfterUpdate
    {
        public int IsStatus { get; set; } = 1;
        public string LinkStart { get; set; } = "";
        public string LinkEnd { get; set; } = "";
        public int EpisodeMoment { get; set; } = 0;
    }
}
