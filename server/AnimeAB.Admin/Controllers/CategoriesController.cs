using AnimeAB.Application.Common.Interface.Reponsitories.Base;
using AnimeAB.Core.Validator.Filter;
using AnimeAB.Domain.Entities;
using AnimeAB.Domain.ValueObject;
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
    [Route("anime/categories")]
    [Authorize(Policy = RoleSchemes.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> ListCategories()
        {
            return Ok(new { data = await unitOfWork.CategoriesEntity.GetCategoriesAsync() });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categories categories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await unitOfWork.CategoriesEntity.CreateCategoryAsync(categories);
                    if (!result.Success) return BadRequest(result.Message);

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
        public async Task<IActionResult> Edit(Categories categories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await unitOfWork.CategoriesEntity.UpdateCategoryAsync(categories);
                    if (!result.Success) return BadRequest(result.Message);
                    return Ok(result.Data);
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
            var result = await unitOfWork.CategoriesEntity.DeleteCategoryAsync(key);
            if (!result.Success) return NotFound();
            return NoContent();
        }
    }
}
