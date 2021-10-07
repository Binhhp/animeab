using AnimeAB.Domain.Entities;
using AnimeAB.Domain.ValueObject;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.Controllers
{
    [Route("cache")]
    [Authorize(Policy = RoleSchemes.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CacheController : Controller
    {
        private readonly IMemoryCache _cache;

        public CacheController(IMemoryCache cache)
        {
            _cache = cache;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("all")]
        public IActionResult GetCaches()
        {
            string animeCache = CacheKeys.AnimeEntry;
            string categoriesCache = CacheKeys.CategoriesEntry;
            string collectsCache = CacheKeys.CollectionsEntry;
            var cache = new List<CacheEntry>();
            if (_cache.TryGetValue(animeCache, out List<Animes> cacheEntry))
                cache.Add(new CacheEntry { Key = animeCache });
            if (_cache.TryGetValue(categoriesCache, out List<Categories> cacheCateEntry))
                cache.Add(new CacheEntry { Key = categoriesCache });
            if (_cache.TryGetValue(collectsCache, out List<Collections> collectsCacheEntry))
                cache.Add(new CacheEntry { Key = collectsCache });

            return Json(new { data = cache });
        }
        [HttpDelete]
        public IActionResult DeleteCache([FromQuery]string cacheKey)
        {
            try
            {
                _cache.Remove(cacheKey);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class CacheEntry
    {
        public string Key { get; set; }
    }
}
