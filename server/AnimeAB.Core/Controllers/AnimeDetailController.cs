
using AnimeAB.AppAdmin.AnimeEndpoints;
using AnimeAB.Application.Reponsitories.Base;
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
using System.Threading.Tasks;

namespace AnimeAB.AppAdmin.Controllers
{
    [Route("anime/{animeKey}")]
    [Authorize(Policy = RoleSchemes.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AnimeDetailController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _enviroment;
        private readonly IMapper _mapper;

        public AnimeDetailController(IUnitOfWork unitOfWork, IWebHostEnvironment enviroment, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _enviroment = enviroment;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(string animeKey)
        {
            ViewData["animeKey"] = animeKey;
            return View();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAnimeDetails(string animeKey)
        {
            IEnumerable<AnimeDetailed> detaileds = await unitOfWork.AnimeDetailEntity.GetCurrentAnimeAsync(animeKey);
            return Ok(new { data = detaileds });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnimeDetailRequest animeDetailDto, string animeKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(string.IsNullOrWhiteSpace(animeDetailDto.Image) && animeDetailDto.FileUpload == null)
                    {
                        return BadRequest("Bạn cần thêm Url ảnh hoặc upload ảnh");
                    }
                    AnimeDetailed item = _mapper.Map<AnimeDetailed>(animeDetailDto);

                    if (animeDetailDto.FileUpload != null)
                    {
                        string uploads = Path.Combine(_enviroment.WebRootPath, $"image");
                        string filePath = Path.Combine(uploads, animeDetailDto.FileUpload.FileName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await animeDetailDto.FileUpload.CopyToAsync(fileStream);
                        }
                        using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        {
                            item.FileName = animeDetailDto.FileUpload.FileName;

                            var result = await unitOfWork.AnimeDetailEntity.CreateMovieAsync(item, fs, animeKey);
                            if (!result.Success) return BadRequest(result.Message);

                            fs.Close();
                        }

                        System.IO.File.Delete(filePath);
                    }
                    else
                    {
                        var result = await unitOfWork.AnimeDetailEntity.CreateMovieAsync(item, null, animeKey);
                        if (!result.Success) return BadRequest(result.Message);
                    }
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit(AnimeDetailRequest animeDetailDto, string animeKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(animeDetailDto.FileUpload == null && string.IsNullOrWhiteSpace(animeDetailDto.Image))
                    {
                        return BadRequest("Bạn cần nhập url ảnh hoặc upload ảnh");
                    }

                    AnimeDetailed item = _mapper.Map<AnimeDetailed>(animeDetailDto);
                    if (animeDetailDto.FileUpload != null)
                    {
                        if (animeDetailDto.FileUpload.Length == 0) return BadRequest();

                        string uploads = Path.Combine(_enviroment.WebRootPath, $"image");
                        string filePath = Path.Combine(uploads, animeDetailDto.FileUpload.FileName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await animeDetailDto.FileUpload.CopyToAsync(fileStream);
                        }

                        using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        {
                            item.FileName = animeDetailDto.FileUpload.FileName;
                            var result = await unitOfWork.AnimeDetailEntity.UpdateMovieAsync(item, fs, animeKey);
                            if (!result.Success) return BadRequest(result.Message);

                            fs.Close();

                            System.IO.File.Delete(filePath);
                            return Ok(result.Data);
                        }
                    }
                    else
                    {
                        var result = await unitOfWork.AnimeDetailEntity.UpdateMovieAsync(item, null, animeKey);
                        return Ok(result.Data);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{animeDetailKey}")]
        public async Task<IActionResult> Delete(string animeKey, string animeDetailKey)
        {
            var result = await unitOfWork.AnimeDetailEntity.DeleteMovieAsync(animeKey, animeDetailKey);
            if (!result.Success) return NotFound();
            return NoContent();
        }
    }

}
