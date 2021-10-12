using AnimeAB.AppAdmin.AnimeEndpoints;
using AnimeAB.Application.Reponsitories.Base;
using AnimeAB.Domain;
using AnimeAB.Domain.DTOs;
using AnimeAB.Domain.Entities;
using AnimeAB.Domain.ValueObjects;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AnimeAB.AppAdmin.Controllers
{
    [Route("anime/movies")]
    [Authorize(Policy = RoleSchemes.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AnimeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _enviroment;
        private readonly IMapper _mapper;

        public AnimeController(IUnitOfWork unitOfWork, IWebHostEnvironment enviroment, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _enviroment = enviroment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collections = await unitOfWork.CollectionEntity.GetCollectionsAsync();
            var categories = await unitOfWork.CategoriesEntity.GetCategoriesAsync();
            ViewData["Collections"] = collections;
            ViewData["Categories"] = categories;
            return View();
        }

        [HttpPost]
        [Route("all")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAnimes([FromBody]AnimeDtoFilter filter)
        {
            var list = await unitOfWork.AnimeEntity.GetAnimesAsync();

            if (!string.IsNullOrWhiteSpace(filter.Category) && filter.Category != "all")
            {
                list = list.Where(x => x.Categories.ContainsKey(filter.Category)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(filter.Collection) && filter.Collection != "all")
            {
                list = list.Where(x => x.CollectionId.Equals(filter.Collection)).ToList();
            }

            if (filter.Status > 0)
            {
                list = list.Where(x => x.IsStatus.Equals(filter.Status)).ToList();
            }

            if (filter.Time > 0)
            {
                if(filter.Time == 1)
                {
                    list = list.OrderBy(x => x.DateRelease).ToList();
                }
                else
                {
                    list = list.OrderByDescending(x => x.DateRelease).ToList();
                }
            }

            return Ok(new { data = list });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnimeRequest anime)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (anime.FileUpload == null && string.IsNullOrWhiteSpace(anime.Image))
                        return BadRequest("Bạn cần upload ảnh hoặc thêm url ảnh");

                    Animes item = _mapper.Map<Animes>(anime);

                    var categories = await unitOfWork.CategoriesEntity.GetCategoriesAsync();
                    var cateUpdate = categories.Where(x => anime.Categories.Contains(x.Key)).ToDictionary(p => p.Key, p => p);
                    item.Categories = cateUpdate;

                    if (anime.FileUpload != null)
                    {
                        string uploads = Path.Combine(_enviroment.WebRootPath, $"image");
                        string filePath = Path.Combine(uploads, anime.FileUpload.FileName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await anime.FileUpload.CopyToAsync(fileStream);
                        }
                        using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        {
                            item.FileName = anime.FileUpload.FileName;
                            var result = await unitOfWork.AnimeEntity.CreateAnimeAsync(item, fs);

                            fs.Close();
                            System.IO.File.Delete(filePath);

                            if (!result.Success) return BadRequest(result.Message);
                        }
                    }
                    else
                    {
                        var result = await unitOfWork.AnimeEntity.CreateAnimeAsync(item, null);
                        if (!result.Success)
                        {
                            return BadRequest(result.Message);
                        }
                    }

                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{key}")]
        public async Task<IActionResult> Edit(AnimeRequest animeDto, string key)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AnimesDomain item = _mapper.Map<AnimesDomain>(animeDto);

                    var categories = await unitOfWork.CategoriesEntity.GetCategoriesAsync();
                    var cateUpdate = categories.Where(x => animeDto.Categories.Contains(x.Key)).ToDictionary(p => p.Key, p => p);
                    item.Categories = cateUpdate;

                    if (animeDto.FileUpload != null)
                    {
                        if (animeDto.FileUpload.Length == 0) return BadRequest();

                        string uploads = Path.Combine(_enviroment.WebRootPath, $"image");
                        string filePath = Path.Combine(uploads, animeDto.FileUpload.FileName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await animeDto.FileUpload.CopyToAsync(fileStream);
                        }

                        using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        {
                            item.FileName = animeDto.FileUpload.FileName;
                            var result = await unitOfWork.AnimeEntity.UpdateAnimeAsync(item, fs);

                            fs.Close();

                            System.IO.File.Delete(filePath);

                            if (!result.Success) return BadRequest(result.Message);
                            return Ok(result.Data);
                        }
                    }
                    else
                    {
                        var result = await unitOfWork.AnimeEntity.UpdateAnimeAsync(item, null);
                        if(!result.Success) return BadRequest(result.Message);
                        return Ok(result.Data);
                    }
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            var result = await unitOfWork.AnimeEntity.DeleteAnimeAsync(key);
            if (!result.Success) return NotFound();
            return NoContent();
        }

        [HttpPost]
        [Route("{key}/banner")]
        public async Task<IActionResult> UpdateBanner(string key, [FromForm]IFormFile file)
        {
            if (file == null) return BadRequest();

            string uploads = Path.Combine(_enviroment.WebRootPath, $"image");
            string filePath = Path.Combine(uploads, file.FileName);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                var result = await unitOfWork.AnimeEntity.UpdateBannerAsync(key, file.FileName, fs);

                fs.Close();

                System.IO.File.Delete(filePath);

                if (!result.Success) return BadRequest(result.Message);

                return Ok(result.Data);
            }
        }

        [HttpGet]
        [Route("{key}/banner")]
        public async Task<IActionResult> DestroyBanner(string key)
        {
            var result = await unitOfWork.AnimeEntity.DestroyBannerAsync(key);
            if (!result.Success) return BadRequest(result.Message);
            return NoContent();
        }
    }
}
