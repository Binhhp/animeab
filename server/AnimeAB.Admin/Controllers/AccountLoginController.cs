using AnimeAB.Application.Common.Interface.Reponsitories.Base;
using AnimeAB.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.Controllers
{
    [Route("anime")]
    public class AccountLoginController : Controller
    {
        public readonly IAppContext appContext;
        private readonly IUnitOfWork unitOfWork;

        public AccountLoginController(IAppContext appContext, IUnitOfWork unitOfWork)
        {
            this.appContext = appContext;
            this.unitOfWork = unitOfWork;
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginDto account)
        {
            if (ModelState.IsValid)
            {
                var result = await unitOfWork.AccountEntity.SignInEmailPasswordAsync(account);

                if (result.Success == false)
                {
                    return BadRequest(result.Message);
                }

                var userLogined = (ProfileDto)result.Data;
                if (!userLogined.IsEmailVerified)
                {
                    return BadRequest("Email cần xác nhận.");
                }
                var auth = 
                    await appContext.CreateUserClaimAsync(userLogined.Email, userLogined.DisplayName, userLogined.PhotoUrl, userLogined.Token, userLogined.Role, userLogined.LocalId);

                return Ok(RedirectToLocal(account.ReturnUrl));
            }
            return Ok(account.ReturnUrl);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            appContext.SignOut();
            return Redirect("~/anime/login");
        }

        private string RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl) && returnUrl != "/")
                return returnUrl;
            else
                return "/anime/dashboard";

        }
    }
}
