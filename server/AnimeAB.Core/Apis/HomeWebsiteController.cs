using System.Text.RegularExpressions;
using AnimeAB.Reponsitories.Domain;
using AnimeAB.Reponsitories.Entities;
using AnimeAB.Reponsitories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AnimeAB.Reponsitories.DTO;
using Microsoft.Extensions.Caching.Memory;
using AnimeAB.Core.CacheMemory;

namespace AnimeAB.Core.Apis
{

    [Route("api")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class HomeWebsiteController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMemoryCache _cache;
        public HomeWebsiteController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            this.unitOfWork = unitOfWork;
            this._cache = cache;
        }

        [Route("animes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animes>>> GetAnimes
            ([FromQuery]AnimeFilterRoot root)
        {
            try
            {
                List<Animes> animes = new List<Animes>();
                if (!_cache.TryGetValue(CacheKeys.AnimeEntry, out List<Animes> cacheEntry))
                {
                    cacheEntry = await unitOfWork.AnimeEntity.GetAnimesAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(7));
                    _cache.Set(CacheKeys.AnimeEntry, cacheEntry, cacheEntryOptions);
                }

                animes = cacheEntry;
                //filter keyword
                if(!string.IsNullOrWhiteSpace(root.q)){

                    Regex rg = new Regex(root.q.ToLower());
                    animes = animes.Where(x => (rg.Matches(x.Title.ToLower())).Count() > 0 || (rg.Matches(x.TitleVie.ToLower())).Count() > 0).ToList();
                }
                //filter category
                if (!string.IsNullOrWhiteSpace(root.cate))
                {
                    if (root.offer) animes = animes.Where(x => !x.CategoryKey.Equals(root.cate)).ToList();
                    else animes = animes.Where(x => x.CategoryKey.Equals(root.cate)).ToList();
                }
                //filter key anime
                if(!string.IsNullOrWhiteSpace(root.id))
                {
                    animes = animes.Where(x => !x.Key.Equals(root.id)).ToList();
                }
                //filter collection 
                if(!string.IsNullOrWhiteSpace(root.collect))
                {
                    animes = animes.Where(x => x.CollectionId.Equals(root.collect)).ToList();
                }
                //filter isStatus
                if(root.stus > 0)
                {
                    animes = animes.Where(x => x.IsStatus < root.stus || x.IsStatus == root.stus).ToList();
                }
                if(root.completed > 0)
                {
                    animes = animes.Where(x => x.IsStatus == root.completed).ToList();
                }
                if(root.trend > 0)
                {
                    animes = animes.Where(x => x.IsStatus > root.trend || x.IsStatus == root.trend).ToList();
                }
                //filter banner
                if (root.banner)
                {
                    animes = animes.Where(x => x.IsBanner == true && x.IsStatus > 1).ToList();
                }
                //sort orderby
                if (!string.IsNullOrWhiteSpace(root.asc))
                {
                    if (root.asc.Equals("views")) animes = animes.OrderBy(x => x.Views).ToList();
                    if (root.asc.Equals("date")) animes = animes.OrderBy(x => x.DateRelease).ToList();
                    if (root.asc.Equals("viewDay")) animes = animes.OrderBy(x => x.ViewDays).ToList();
                    if (root.asc.Equals("viewWeek")) animes = animes.OrderBy(x => x.ViewWeeks).ToList();
                    if (root.asc.Equals("viewMonth")) animes = animes.OrderBy(x => x.ViewMonths).ToList();
                }
                //sort order by descending
                if (!string.IsNullOrWhiteSpace(root.des))
                {
                    if (root.des.Equals("views")) animes = animes.OrderByDescending(x => x.Views).ToList();
                    if (root.des.Equals("date")) animes = animes.OrderByDescending(x => x.DateRelease).ToList();
                    if (root.des.Equals("viewDay")) animes = animes.OrderByDescending(x => x.ViewDays).ToList();
                    if (root.des.Equals("viewWeek")) animes = animes.OrderByDescending(x => x.ViewWeeks).ToList();
                    if (root.des.Equals("viewMonth")) animes = animes.OrderByDescending(x => x.ViewMonths).ToList();
                }
                //take and random
                if (root.random && root.take > 0)
                {
                    var animesRandom = new List<Animes>();
                    Random r = new Random();
                    for(var i = 0; i <= root.take; i++)
                    {
                        if(animesRandom.Count > 0)
                        {
                            while (true)
                            {
                                var index = r.Next(0, animes.Count);
                                var anisRandom = animes[index];
                                var checkItem = animesRandom.FirstOrDefault(x => x.Key.Equals(anisRandom.Key));
                                if (checkItem == null)
                                {
                                    animesRandom.Add(anisRandom);
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            var index = r.Next(0, animes.Count);
                            var anisRandom = animes[index];
                            animesRandom.Add(anisRandom);
                        }
                    }

                    animes = animesRandom;
                }
                //take index
                if(root.take > 0 && root.random == false)
                {
                    animes = animes.Take(root.take).ToList();
                }
                return Ok(animes);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("animes")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Animes>>> GetAnimesFiltered(
            [FromQuery]string keyword, [FromBody]AnimeFilter filter)
        {
            try
            {
                List<Animes> animes = new List<Animes>();
                if (!_cache.TryGetValue(CacheKeys.AnimeEntry, out List<Animes> cacheEntry))
                {
                    cacheEntry = await unitOfWork.AnimeEntity.GetAnimesAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(7));
                    _cache.Set(CacheKeys.AnimeEntry, cacheEntry, cacheEntryOptions);
                }

                animes = cacheEntry;

                if (!string.IsNullOrWhiteSpace(keyword)){
                    Regex rg = new Regex(keyword.ToLower());
                    animes = animes.Where(x => (rg.Matches(x.Title.ToLower())).Count() > 0 || (rg.Matches(x.TitleVie.ToLower())).Count() > 0).ToList();
                }
                if(filter.CategoryFilters.Count() > 0)
                {
                    animes = animes.Where(x => filter.CategoryFilters.Contains(x.CategoryKey)).OrderByDescending(x => x.Views).ToList();
                }
                if(filter.CollectFilters.Count() > 0)
                {
                    animes = animes.Where(x => filter.CollectFilters.Contains(x.CollectionId)).OrderByDescending(x => x.Views).ToList();
                }

                return Ok(animes.ToList());
            }
            catch
            {
                return BadRequest();
            }
        }


        [Route("animes/{animeKey}/views")]
        [HttpGet]
        public async Task<IActionResult> UpdateViewAnime([FromRoute]string animeKey)
        {
            try
            {
                await unitOfWork.AnimeEntity.UpdateViewAsync(animeKey);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("animes/{animeKey}/{animeDetailKey}/views")]
        [HttpGet]
        public async Task<IActionResult> UpdateViewAnime([FromRoute]string animeKey, [FromRoute]string animeDetailKey)
        {
            try
            {
                await unitOfWork.AnimeDetailEntity.UpdateViewAsync(animeKey, animeDetailKey);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("animes/episodes")]
        [HttpGet]
        public async Task<IActionResult> GetAnimeEpisode
            ([FromQuery]string id, [FromQuery]string episode = "")
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(episode))
                {
                    AnimeDetailed episodeCurrent = await unitOfWork.AnimeDetailEntity.GetAnimeDetailAsync(id, episode);
                    return Ok(episodeCurrent);
                }
                else
                {
                    IEnumerable<AnimeDetailed> animeDetaileds = await unitOfWork.AnimeDetailEntity.GetCurrentAnimeAsync(id);
                    return Ok(animeDetaileds);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("animes/{animeKey}")]
        [HttpGet]
        public async Task<IActionResult> GetAnimeDetail([FromRoute]string animeKey)
        {
            try
            {
                var anime = await unitOfWork.AnimeEntity.GetCurentAnimeAsync(animeKey);
                return Ok(anime);
            }
            catch
            {
                return BadRequest();
            }
        }
        [Route("collections")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collections>>> GetCollections()
        {
            try
            {
                IEnumerable<Collections> collections = new List<Collections>();
                if (!_cache.TryGetValue(CacheKeys.CollectionsEntry, out IEnumerable<Collections> cacheEntry))
                {
                    cacheEntry = await unitOfWork.CollectionEntity.GetCollectionsAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(365));
                    _cache.Set(CacheKeys.CollectionsEntry, cacheEntry, cacheEntryOptions);
                }
                collections = cacheEntry;

                return Ok(collections);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("categories")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            try
            {
                IEnumerable<Categories> categories = new List<Categories>();
                if (!_cache.TryGetValue(CacheKeys.CollectionsEntry, out IEnumerable<Categories> cacheEntry))
                {
                    cacheEntry = await unitOfWork.CategoriesEntity.GetCategoriesAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(365));
                    _cache.Set(CacheKeys.CategoriesEntry, cacheEntry, cacheEntryOptions);
                }
                categories = cacheEntry;
                return Ok(categories);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("series/{series}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeSeries>>> GetAnimeSeries(string series)
        {
            try
            {
                var animeSeries = await unitOfWork.AnimeSeries.GetCurentSeriesAsync(series);
                return animeSeries;
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
