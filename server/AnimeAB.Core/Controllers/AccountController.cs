using AnimeAB.Reponsitories.Domain;
using AnimeAB.Reponsitories.DTO;
using AnimeAB.Reponsitories.Interface;
using AnimeAB.Reponsitories.Utils;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimeAB.Core.Controllers
{
    [Authorize(Policy = RoleSchemes.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("anime")]
    public class AccountController : Controller
    {
        public readonly IUnitOfWork unitOfWork;
        public readonly IAppContext appContext;
        private readonly IWebHostEnvironment _environment;

        public AccountController(IUnitOfWork unitOfWork, IAppContext appContext, IWebHostEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            this.appContext = appContext;
            _environment = environment;
        }

        [Route("signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult SignUp(AccountSignUpDto account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = unitOfWork.AccountEntity.CreateEmailPasswordAsync(account);
                    if (!result.Success)
                    {
                        return BadRequest(result.Message);
                    }
                }

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("users")]
        public IActionResult ListAccount()
        {
            return View();
        }

        [Route("accounts")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var list = await unitOfWork.AccountEntity.GetUsersAsync();
            return Json(new { data = list });
        }

        [Route("accounts")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteAccount([FromBody]UserDelete user)
        {
            var userLogined = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userLogined == user.userId) return BadRequest("Tài khoản đang đăng nhập không thể xóa");

            var result = await unitOfWork.AccountEntity.DeleteUserAsync(user.token, user.userId);

            if (!result) return BadRequest("Bad request 400");

            return NoContent();
        }

        [Route("profile")]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("profile")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileUpdate profile)
        {
            if (profile.FileUpload.Length == 0) return BadRequest();
            if (string.IsNullOrWhiteSpace(profile.DisplayName) || string.IsNullOrWhiteSpace(profile.UserToken))
                return BadRequest();

            string uploads = Path.Combine(_environment.WebRootPath, $"image");
            string filePath = Path.Combine(uploads, profile.FileUpload.FileName);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profile.FileUpload.CopyToAsync(fileStream);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open)){
                    var result = await unitOfWork.AccountEntity.UpdateProfileAsync(new ProfileDomain
                    {
                        Token = profile.UserToken,
                        DisplayName = profile.DisplayName,
                        StreamAvatar = fs,
                        FileName = profile.FileUpload.FileName
                    });

                if (!result.Success) return BadRequest(result.Message);

                var profileUpdated = (ProfileDto)result.Data;
                var updateClaim = appContext.UpdateUserClaim(User.Identity as ClaimsIdentity, profileUpdated.DisplayName, profileUpdated.PhotoUrl);
                fs.Close();

                System.IO.File.Delete(filePath);
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("password/{email}")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = unitOfWork.AccountEntity.ForgotPasswordAsync(email);
                if (!result.Success)
                    return BadRequest(result.Message);

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("confirm-email")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmEmail()
        {
            return View();
        }
    }
    public class UserDelete
    {
        public string userId { get; set; }
        public string token { get; set; }
    }

    public class ProfileUpdate
    {
        public string UserToken { get; set; }
        public string DisplayName { get; set; }
        public IFormFile FileUpload { get; set; }
    }
}
