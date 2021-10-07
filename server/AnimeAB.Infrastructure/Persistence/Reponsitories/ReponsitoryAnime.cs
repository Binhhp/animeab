
using AnimeAB.Application.Common.Behaviour;
using AnimeAB.Application.Common.Interface.Reponsitories;
using AnimeAB.Domain;
using AnimeAB.Domain.DTOs;
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

    public class ReponsitoryAnime : Request, IReponsitoryAnime
    {
        private readonly AppSettingFirebase _appSetting;
        private readonly IFirebaseClient database;
        private readonly FirebaseStorage storage;

        public ReponsitoryAnime(AppSettingFirebase appSetting)
        {
            _appSetting = appSetting;
            database = FirebaseManager.Database(_appSetting.AuthSecret, _appSetting.DatabaseURL);
            storage = FirebaseManager.Storage(_appSetting.StorageBucket);
        }
        /// <summary>
        /// Update view
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateViewAsync(string key)
        {
            try
            {
                var data = await database.GetAsync(Table.ANIME + "/" + key);
                if (data.Body == "null") return false;

                AnimeView updateView = data.ResultAs<AnimeView>();
                var date = (DateTime)updateView.DateCreated;
                if (date.ToString("MM/dd/yyyy") == DateTime.UtcNow.ToString("MM/dd/yyyy"))
                {
                    updateView.Views += 1;
                    updateView.ViewDays += 1;
                    updateView.ViewMonths += 1;
                    updateView.ViewWeeks += 1;
                }
                else
                {
                    Random r = new Random();
                    updateView.DateCreated = DateTime.UtcNow;
                    updateView.Views = r.Next(50000, 250000);
                    updateView.ViewDays = r.Next(50, 800);
                    updateView.ViewWeeks = r.Next(801, 1500);
                    updateView.ViewMonths = r.Next(1501, 4000);
                }
                await database.UpdateAsync(Table.ANIME + "/" + key, updateView);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get curent anime
        /// </summary>
        /// <param name="animeKey"></param>
        /// <returns></returns>
        public async Task<Animes> GetCurentAnimeAsync(string animeKey)
        {
            try
            {
                var anime = new Animes();
                var data = await database.GetAsync(Table.ANIME + "/" + animeKey);
                if (data.Body == "null") return anime;

                anime = data.ResultAs<Animes>();
                return anime;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// List animes
        /// </summary>
        /// <returns></returns>
        public async Task<List<Animes>> GetAnimesAsync()
        {
            try
            {
                var data = await database.GetAsync(Table.ANIME);
                if (data.Body == "null") return new List<Animes>();

                Dictionary<string, Animes> contexts = data.ResultAs<Dictionary<string, Animes>>();
                var animes = contexts.Values.OrderByDescending(x => x.DateRelease).OrderBy(x => x.IsStatus).ToList();
                return animes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Create animes
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        public async Task<Response> CreateAnimeAsync(Animes anime, Stream file)
        {
            try
            {
                var data = await database.GetAsync(Table.ANIME + "/" + anime.Key);
                if (data.Body != "null") return Error("Bản ghi đã tồn tại.");

                if(file != null)
                {
                    var cancellation = new CancellationTokenSource();

                    await storage.Child(Table.ANIME).Child(anime.FileName).PutAsync(file, cancellation.Token);
                    var pathImage = await storage.Child(Table.ANIME).Child(anime.FileName).GetDownloadUrlAsync();
                    anime.Image = pathImage;
                }

                Random r = new Random();

                anime.IsBanner = false;
                anime.Views = r.Next(50000, 250000);
                anime.ViewDays = r.Next(50, 800);
                anime.ViewWeeks = r.Next(801, 1500);
                anime.ViewMonths = r.Next(1501, 4000);
                anime.DateCreated = DateTime.UtcNow;
                anime.IsStatus = 1;

                await database.SetAsync(Table.ANIME + "/" + anime.Key, anime);
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
        public async Task<Response> UpdateAnimeAsync(AnimesDomain anime, Stream image)
        {
            try
            {
                var data = await database.GetAsync(Table.ANIME + "/" + anime.Key);

                if (data.Body == "null") return Error("Bản ghi không tồn tại.");
                Animes ani = data.ResultAs<Animes>();

                if (image != null)
                {
                    var cancellation = new CancellationTokenSource();

                    var deleteImageOld = Task.Run(() => storage.Child(Table.ANIME).Child(ani.FileName).DeleteAsync());
                    var uploadImageNew = Task.Run(() => storage.Child(Table.ANIME).Child(anime.FileName).PutAsync(image, cancellation.Token));
                    await Task.WhenAll(deleteImageOld, uploadImageNew);

                    var pathImage = await storage.Child(Table.ANIME).Child(anime.FileName).GetDownloadUrlAsync();

                    anime.Image = pathImage;

                }
                else
                {
                    if (anime.Image.IndexOf("animeab.appspot.com") > -1)
                    {
                        anime.FileName = ani.FileName;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(ani.FileName))
                        {
                            await storage.Child(Table.ANIME).Child(ani.FileName).DeleteAsync();
                        }
                        anime.FileName = "";
                    }
                }

                var animeDetails = await database.GetAsync(Table.ANIME + "/" + anime.Key + "/" + Table.ANIMEDETAIL);
                if(animeDetails.Body != "null")
                {
                    int items = (animeDetails.ResultAs<Dictionary<string, AnimeDetailed>>()).Count();
                    if(anime.Episode <= items)
                    {
                        anime.Episode = items;
                        anime.IsStatus = 3;
                    }
                    else
                    {
                        anime.IsStatus = 2;
                    }
                }
                else
                {
                    anime.IsStatus = 1;
                }
                await database.UpdateAsync(Table.ANIME + "/" + ani.Key, anime);

                return Success(anime);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// Delete animes
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Response> DeleteAnimeAsync(string key)
        {
            try
            {
                
                var anime = await database.GetAsync(Table.ANIME + "/" + key);

                if (anime.Body == "null") return Error("Bản ghi không tồn tại.");
                Animes anie = anime.ResultAs<Animes>();

                var animeDetail = await database.GetAsync(Table.ANIME + "/" + key + "/" + Table.ANIMEDETAIL);
                if (animeDetail.Body != "null")
                {
                    Dictionary<string, AnimeDetailed> list = animeDetail.ResultAs<Dictionary<string, AnimeDetailed>>();
                    await Task.WhenAll(list.Select(item => Task.Run(() => 
                        {
                            if (!string.IsNullOrWhiteSpace(item.Value.FileName))
                            {
                                storage.Child(Table.ANIMEDETAIL).Child(item.Value.FileName).DeleteAsync();
                            }
                        })
                    ));
                }

                var deleteDb = Task.Run(() => database.DeleteAsync(Table.ANIME + "/" + key));
                if (!string.IsNullOrWhiteSpace(anie.FileName))
                {
                    var deleteImageStorage = Task.Run(() => storage.Child(Table.ANIME).Child(anie.FileName).DeleteAsync());
                    await Task.WhenAll(deleteImageStorage, deleteDb);
                }
                else
                {
                    Task.WaitAny(deleteDb);
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// Update banner
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fileName"></param>
        /// <param name="banner"></param>
        /// <returns></returns>
        public async Task<Response> UpdateBannerAsync(string key, string fileName, Stream banner)
        {
            try
            {
                var anime = await database.GetAsync(Table.ANIME + "/" + key);

                if (anime.Body == "null") return Error("Bản ghi không tồn tại.");
                Animes anie = anime.ResultAs<Animes>();

                var cancellation = new CancellationTokenSource();
                await storage.Child(Table.ANIME).Child(fileName).PutAsync(banner, cancellation.Token);
                var pathBanner = await storage.Child(Table.ANIME).Child(fileName).GetDownloadUrlAsync();

                anie.Banner = pathBanner;
                anie.FileNameBanner = fileName;
                anie.IsBanner = true;

                await database.UpdateAsync(Table.ANIME + "/" + anie.Key, anie);
                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// Destroy banner
        /// </summary>
        /// <param name="key">key anime</param>
        /// <returns></returns>
        public async Task<Response> DestroyBannerAsync(string key)
        {
            try
            {
                var anime = await database.GetAsync(Table.ANIME + "/" + key);

                if (anime.Body == "null") return Error("Bản ghi không tồn tại.");
                Animes anie = anime.ResultAs<Animes>();

                anie.Banner = "";
                anie.FileNameBanner = "";
                anie.IsBanner = false;

                var deleteBanner = Task.Run(() => storage.Child(Table.ANIME).Child(anie.FileNameBanner).DeleteAsync());
                var deleteDb = Task.Run(() => database.UpdateAsync(Table.ANIME + "/" + anie.Key, anie));
                await Task.WhenAll(deleteBanner, deleteDb);

                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
