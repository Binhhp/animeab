using AutoAnimeAB.Models;
using Firebase.Storage;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutoAnimeAB.Reponsitory.AnimeDetail
{
    public class ReponsitoryAnimeDetail
    {
        private IFirebaseClient database;

        public ReponsitoryAnimeDetail()
        {
            database = FirebaseManager.Database();
        }
        /// <summary>
        /// Create animes
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        public async Task CreateMovieAsync(AnimeModel movie, string animeKey)
        {
            try
            {

                var animeData = await database.GetAsync(Table.ANIME + "/" + animeKey);
                Animes anime = animeData.ResultAs<Animes>();

                if (anime.IsStatus == 3) return;

                string pathDatabase = Table.PATHANIMEDETAILED(animeKey, movie.Key);

                var data = await database.GetAsync(pathDatabase);
                if (data.Body != "null") return;

                Random r = new Random();
                movie.Views = r.Next(50000, 250000);
                var changeTitle = movie.Title.IndexOf("-");
                if (changeTitle > -1)
                {
                    movie.Title = movie.Title.Substring(changeTitle + 2);
                }

                var uploadMovie = Task.Run(() => database.SetAsync(pathDatabase, movie));

                anime.DateRelease = DateTime.Now;
                anime.EpisodeMoment += 1;
                if (anime.EpisodeMoment == 1)
                {
                    anime.LinkStart = movie.Key;
                }

                if (anime.Episode == anime.EpisodeMoment)
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


                return;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Update link video
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Response> UpdateLinkAsync(string animeKey, List<Episode> episodes)
        {
            try
            {
                var animes = await database.GetAsync(Table.ANIME + "/" + animeKey + "/" + Table.ANIMEDETAIL);
                if (animes.Body == "null")
                    return new Response { Success = false, Error = "Anime không tồn tại"  };

                Dictionary<string, AnimeModel> animeDetails = animes.ResultAs<Dictionary<string, AnimeModel>>();
                var data = animeDetails.Values.ToList();

                if(animeDetails.Count > 0)
                {
                    foreach(var epis in episodes)
                    {
                        var detail = data.FirstOrDefault(x => x.Episode.Equals(epis.Number));
                        if(detail != null && !string.IsNullOrWhiteSpace(epis.Link))
                        {
                            if (epis.Link.IndexOf("femax") > -1) detail.Iframe = true;
                            else
                            {
                                if (epis.Link.IndexOf("fembed") > -1) detail.Iframe = true;
                                else detail.Iframe = false;
                            }

                            detail.Link = epis.Link;
                            await database.UpdateAsync(Table.ANIME + "/" + animeKey + "/" + Table.ANIMEDETAIL + "/" + detail.Key, detail);
                        }
                    }

                    return new Response();
                }
                else
                {
                    return new Response { Error = "Anime cần upload video", Success = false };
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
