
using AnimeAB.AppAdmin.AnimeEndpoints;
using AnimeAB.Application.Reponsitories.Base;
using AnimeAB.Domain.Entities;
using AnimeAB.Domain.ValueObjects;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.AppAdmin.Controllers
{
    [Authorize(Policy = RoleSchemes.Admin, 
        AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("anime/series")]
    public class AnimeSeriesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AnimeSeriesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetSeries()
        {
            var series = await unitOfWork.AnimeSeries.GetSeriesAsync();
            return Ok(new { data = series });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeries(string series)
        {
            series = series.ToLower();
            var result = await unitOfWork.AnimeSeries.CreateSeriesAsync(series);
            return Ok();
        }

        [HttpGet]
        [Route("{series}")]
        public async Task<IActionResult> AnimeSeries(string series)
        {
            var animes = await unitOfWork.AnimeEntity.GetAnimesAsync();
            ViewData["Series"] = series;
            ViewData["Animes"] = animes;
            return View();
        }

        [HttpGet]
        [Route("{series}/all")]
        public async Task<IActionResult> GetAnimeSeries(string series)
        {
            try
            {
                List<Animes> animes = await unitOfWork.AnimeEntity.GetAnimesAsync();
                animes = animes.Where(x => x.Series.Equals(series)).ToList();

                if (animes.Count == 0) return Ok(new { data = new List<AnimeSeriesResponse>() });

                IEnumerable<AnimeSeriesResponse> animeSeries = mapper.Map<IEnumerable<AnimeSeriesResponse>>(animes);

                return Ok(new { data = animeSeries });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{series}")]
        public async Task<IActionResult> DeleteSeries(string series)
        {
            try
            {
                List<Animes> animes = await unitOfWork.AnimeEntity.GetAnimesAsync();

                await unitOfWork.AnimeSeries.DeleteSeriesAsync(series, animes);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("{series}")]
        public IActionResult CreateAnimeSeries(string series, string[] idAnimes)
        {
            var result = unitOfWork.AnimeSeries.CreateAnimeSeriesAsync(series, idAnimes);
            if (!result)
            {
                return BadRequest("Anime đã được thêm vào series");
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{series}/{idAnime}")]
        public IActionResult DeleteAnimeSeries(string idAnime)
        {
            try
            {
                unitOfWork.AnimeSeries.DeleteAnimeSeriesAsync(idAnime);
                return Ok();
            }
            catch
            {
                return BadRequest("Interval Server Error");
            }
        }
    }
}
