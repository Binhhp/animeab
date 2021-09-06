using AnimeAB.Reponsitories.Entities;
using AnimeAB.Reponsitories.Interface;
using AnimeAB.Reponsitories.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.Controllers
{
    [Authorize(Policy = RoleSchemes.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
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
                var data = await unitOfWork.AnimeSeries.GetCurentSeriesAsync(series);
                return Ok(new { data = data });
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
                await unitOfWork.AnimeSeries.DeleteSeriesAsync(series);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("{series}")]
        public async Task<IActionResult> CreateAnimeSeries(string series, AnimeSeries animeSeries)
        {
            var anime = await unitOfWork.AnimeEntity.GetCurentAnimeAsync(animeSeries.Key);
            animeSeries.LinkEnd = anime.LinkEnd;
            animeSeries.LinkStart = anime.LinkStart;

            var result = await unitOfWork.AnimeSeries.CreateAnimeSeriesAsync(series, animeSeries);
            if (!result)
            {
                return BadRequest("Anime đã được thêm vào series");
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{series}/{animeSeries}")]
        public async Task<IActionResult> DeleteAnimeSeries(string series, string animeSeries)
        {
            try
            {
                await unitOfWork.AnimeSeries.DeleteAnimeSeriesAsync(series, animeSeries);
                return Ok();
            }
            catch
            {
                return BadRequest("Interval Server Error");
            }
        }
    }
}
