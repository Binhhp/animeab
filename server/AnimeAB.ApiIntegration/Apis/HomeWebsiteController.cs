using System.Text.RegularExpressions;
using AnimeAB.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using AnimeAB.Domain.DTOs;
using AnimeAB.Application.Common.Interface.Reponsitories.Base;
using AnimeAB.Domain;
using AnimeAB.ApiIntegration.Filters;
using AnimeAB.Infrastructure.ApiResponse;
using AnimeAB.Domain.ValueObject;

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
        private readonly IMapper _mapper;
        public HomeWebsiteController(
            IUnitOfWork unitOfWork, 
            IMemoryCache cache, 
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _cache = cache;
            _mapper = mapper;
        }

        [Route("animes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeResponse>>> GetAnimes
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
                //filter category
                if (!string.IsNullOrWhiteSpace(root.cate))
                {
                    if (root.cate.IndexOf(",") > -1)
                    {
                        animes = animes.Where(x => x.Categories.Values.Any(x => root.cate.IndexOf(x.Key) > -1)).ToList();
                    }
                    else
                    {
                        animes = animes.Where(x => x.Categories.ContainsKey(root.cate)).ToList();
                    }
                }

                //filter key anime
                if (!string.IsNullOrWhiteSpace(root.id))
                    animes = animes.Where(x => !x.Key.Equals(root.id)).ToList();
                //filter collection 
                if (!string.IsNullOrWhiteSpace(root.collect))
                    animes = animes.Where(x => x.CollectionId.Equals(root.collect)).ToList();
                //filter isStatus
                if (!string.IsNullOrWhiteSpace(root.status))
                    animes = animes.FilterStatus(root.status).ToList();

                //filter banner
                if (root.banner)
                    animes = animes.Where(x => x.IsBanner == true && x.IsStatus > 1).ToList();

                if (!string.IsNullOrWhiteSpace(root.type))
                    animes = animes.FilterType(root.type).ToList();

                //rank
                if (root.rank)
                {
                    AnimeTrending animeTrending = animes.GetRanks(_mapper);
                    return Ok(animeTrending);
                }
                //sort orderby
                if (!string.IsNullOrWhiteSpace(root.sort_by) && !string.IsNullOrWhiteSpace(root.order))
                    animes = animes.SingleSort(root.sort_by, root.order);

                if (!string.IsNullOrWhiteSpace(root.sort))
                    animes = animes.MultipleSort(root.sort);

                //random element
                if (root.random > 0)
                    animes = animes.Random(root.random, root.id);

                //take index
                if (root.take > 0)
                    animes = animes.Take(root.take).ToList();

                IEnumerable<AnimeResponse> responses = _mapper.Map<IEnumerable<AnimeResponse>>(animes);
                return Ok(responses);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("animes")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AnimeResponse>>> PostAnimeFilter(
            [FromBody]AnimeFilter filter)
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
                if (!string.IsNullOrWhiteSpace(filter.q))
                {

                    Regex rg = new Regex(filter.q.ToLower());
                    animes = animes.Where(x => (rg.Matches(x.Title.ToLower())).Count() > 0 || (rg.Matches(x.TitleVie.ToLower())).Count() > 0).ToList();
                }
                else
                {
                    animes = animes.OrderByDescending(x => x.DateRelease).ToList();
                }
                //filter cate
                if (!string.IsNullOrWhiteSpace(filter.cate))
                {
                    List<string> cateFilters = filter.cate.Split("+").ToList();
                    animes = animes.Where(x => 
                        x.Categories.Keys.ToList().Intersect(cateFilters).Count() == cateFilters.Count)
                            .OrderByDescending(x => x.DateRelease)
                            .ToList();
                }

                if (!string.IsNullOrWhiteSpace(filter.collect))
                {
                    List<string> collectFilters = filter.collect.Split("+").ToList();
                    animes = animes.Where(x => collectFilters.Contains(x.CollectionId))
                        .OrderByDescending(x => x.DateRelease)
                        .ToList();
                }

                IEnumerable<AnimeResponse> responses = _mapper.Map<IEnumerable<AnimeResponse>>(animes);
                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("animes/watching")]
        [HttpPost]
        public async Task<IActionResult> GetAnimeWatching([FromBody]AnimeWatchingRequest request)
        {
            try
            {
                List<AnimeWatching> filters = new List<AnimeWatching>();
                foreach(var item in request.watching)
                {
                    var ep = item.Split(":").ToArray();
                    var filterItem = new AnimeWatching
                    {
                        episode = ep[1].Contains(ep[0] + "-ep-") ? ep[1].Replace(ep[0] + "-ep-", "") : "1",
                        idAnime = ep[0],
                        link = ep[1]
                    };

                    filters.Add(filterItem);
                }

                List<Animes> animes = new List<Animes>();
                if (!_cache.TryGetValue(CacheKeys.AnimeEntry, out List<Animes> cacheEntry))
                {
                    cacheEntry = await unitOfWork.AnimeEntity.GetAnimesAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(7));
                    _cache.Set(CacheKeys.AnimeEntry, cacheEntry, cacheEntryOptions);
                }

                animes = cacheEntry;

                IEnumerable<AnimeWatchingResponse> responses = 
                    animes.Where(x => filters.FirstOrDefault(w => w.idAnime.Equals(x.Key)) != null)
                        .Select(anis => new AnimeWatchingResponse
                        {
                            Episodes = anis.Episode,
                            EpisodeMoment = Convert.ToInt32(filters.FirstOrDefault(w => w.idAnime.Equals(anis.Key)).episode),
                            Image = anis.Image,
                            Key = anis.Key,
                            Link = filters.FirstOrDefault(w => w.idAnime.Equals(anis.Key)).link,
                            Title = anis.Title,
                            Date = anis.DateRelease
                        }).OrderByDescending(or => or.Date).ToList();

                if(request.take > 0) responses = responses.Take(request.take).ToList();
                return Ok(responses);
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
        public async Task<IActionResult> UpdateViewAnime(
            [FromRoute]string animeKey, [FromRoute]string animeDetailKey)
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
            ([FromQuery]string id, 
            [FromQuery]string episode = "",
            [FromQuery]int sv = 0)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(episode))
                {
                    AnimeDetailed episodeCurrent = 
                        await unitOfWork.AnimeDetailEntity
                             .GetAnimeDetailAsync(id, episode);

                    if(sv == 1)
                    {
                        EpisodeAnimeVsub vuighe = _mapper.Map<EpisodeAnimeVsub>(episodeCurrent);
                        return Ok(vuighe);
                    }

                    if(sv == 2)
                    {
                        EpisodeVuighe vuighe = _mapper.Map<EpisodeVuighe>(episodeCurrent);
                        return Ok(vuighe);
                    }

                    EpisodeResponse response = _mapper.Map<EpisodeResponse>(episodeCurrent);
                    return Ok(response);
                }
                else
                {
                    IEnumerable<AnimeDetailed> animeDetaileds = 
                        await unitOfWork.AnimeDetailEntity.GetCurrentAnimeAsync(id);

                    IEnumerable<EpisodeInstance> responses = 
                        _mapper.Map<IEnumerable<EpisodeInstance>>(animeDetaileds);
                    return Ok(responses);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("animes/{id}")]
        [HttpGet]
        public async Task<ActionResult<AnimeResponse>> GetAnimeDetail([FromRoute]string id)
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
                collections = cacheEntry.ToList();

                var anime = await unitOfWork.AnimeEntity.GetCurentAnimeAsync(id);
                AnimeResponse response = _mapper.Map<AnimeResponse>(anime);
                response.Collection = collections.FirstOrDefault(x => x.Key.Equals(anime.CollectionId)).Title + " " + anime.DateRelease.Year;
                return Ok(response);
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
        public async Task<ActionResult<IEnumerable<AnimeSeriesResponse>>> GetAnimeSeries(string series)
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
                animes = animes.Where(x => x.Series.Equals(series)).OrderByDescending(x => x.DateRelease).ToList();
                if(animes.Count == 0) return NotFound();

                IEnumerable<AnimeSeriesResponse> animeSeries = _mapper.Map<IEnumerable<AnimeSeriesResponse>>(animes);
                return Ok(animeSeries);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
