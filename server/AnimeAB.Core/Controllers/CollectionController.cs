using AnimeAB.Core.Validator.Filter;
using AnimeAB.Core.Validator.Response;
using AnimeAB.Reponsitories.Entities;
using AnimeAB.Reponsitories.Interface;
using AnimeAB.Reponsitories.Utils;
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

namespace AnimeAB.Core.Controllers
{
    [Route("anime/collection")]
    [Authorize(Policy = RoleSchemes.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CollectionController : Controller
    {
        private readonly IWebHostEnvironment _enviroment;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public CollectionController(IWebHostEnvironment enviroment, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _enviroment = enviroment;
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> ListCollection()
        {
            return Ok(new { data = await unitOfWork.CollectionEntity.GetCollectionsAsync() });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CollectionDto collecInsert)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (collecInsert.FileUpload.Length == 0) return BadRequest();

                    string uploads = Path.Combine(_enviroment.WebRootPath, $"image");
                    string filePath = Path.Combine(uploads, collecInsert.FileUpload.FileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await collecInsert.FileUpload.CopyToAsync(fileStream);
                    }

                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        Collections collections = new Collections
                        {
                            Key = collecInsert.Key,
                            DateCreated = DateTime.UtcNow,
                            FileName = collecInsert.FileUpload.FileName,
                            Title = collecInsert.Title
                        };

                        var result = await unitOfWork.CollectionEntity.CreateCollectionAsync(collections, fs);
                        if (!result.Success) return BadRequest(result.Message);

                        fs.Close();
                    }

                    System.IO.File.Delete(filePath);
                    return NoContent();

                }
                else
                {
                    var errorResponse = ValidationHanlder.GetErrors(ModelState);
                    return BadRequest(errorResponse.Errors);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Edit(CollectionDto collectionDto, string key)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Collections item = _mapper.Map<Collections>(collectionDto);
                    if (collectionDto.FileUpload != null)
                    {

                        string uploads = Path.Combine(_enviroment.WebRootPath, $"image");
                        string filePath = Path.Combine(uploads, collectionDto.FileUpload.FileName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await collectionDto.FileUpload.CopyToAsync(fileStream);
                        }

                        using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        {

                            var result = await unitOfWork.CollectionEntity.UpdateCollectionAsync(item, fs);
                            if (!result.Success) return BadRequest(result.Message);

                            fs.Close();

                            System.IO.File.Delete(filePath);
                            return Ok(result.Data);
                        }
                    }
                    else
                    {
                        var result = await unitOfWork.CollectionEntity.UpdateCollectionAsync(item, null);
                        if (!result.Success) return BadRequest(result.Message);
                        return Ok(result.Data);
                    }
                }
                else
                {
                    var errorResponse = ValidationHanlder.GetErrors(ModelState);
                    return BadRequest(errorResponse.Errors);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            var result = await unitOfWork.CollectionEntity.DeleteCollectionAsync(key);
            if (!result.Success) return NotFound();
            return NoContent();
        }
    }

    public class CollectionDto
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public IFormFile FileUpload { get; set; }
    }
}
