
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using AnimeAB.Domain.ValueObject;
using AnimeAB.Application.Common.Interface.Reponsitories.Base;
using AnimeAB.Domain.Common;

namespace AnimeAB.Core.Controllers
{
    [Authorize(Policy = RoleSchemes.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("anime/dashboard")]
        public async Task<IActionResult> Index()
        {
            var animes = await unitOfWork.AnimeEntity.GetAnimesAsync();
            ViewData["Categories"] = (await unitOfWork.CategoriesEntity.GetCategoriesAsync()).Count();
            ViewData["Animes"] = animes.Count();
            ViewData["Collections"] = (await unitOfWork.CollectionEntity.GetCollectionsAsync()).Count();
            ViewData["Users"] = (await unitOfWork.AccountEntity.GetUsersAsync()).Count();
            return View();
        }

        [Route("anime/analysis")]
        public async Task<IActionResult> Analysis()
        {
            var animes = await unitOfWork.AnimeEntity.GetAnimesAsync();
            var monday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var mondayAnimeCreated = animes.Where(x => x.DateCreated.Date == monday.Date).Count();
            return Ok(mondayAnimeCreated);
        }

        [Route("Unauthorized")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("/NotFound")]
        public IActionResult NotFoundView()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("/")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}
