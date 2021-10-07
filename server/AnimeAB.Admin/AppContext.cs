using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AnimeAB.Core
{
    public interface IAppContext
    {
        Task<bool> CreateUserClaimAsync(string email, string name, string avatar, string token, string role, string userId);
        Task<bool> UpdateUserClaim(ClaimsIdentity identity, string name, string avatar);
        void SignOut();
        string GetClaim(string key);
        string UserId { get; }
        string UserName { get; }
        string RoleName { get; }
        string UserAvt { get; }
    }

    public class AppContext : IAppContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AppContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public const string Avatar = "Avatar";
        /// <summary>
        /// Create user claim
        /// </summary>
        /// <param name="email">user id</param>
        /// <param name="name">full name user</param>
        /// <param name="avatar">avatar user</param>
        /// <returns>Create user claim</returns>
        public async Task<bool> CreateUserClaimAsync(string email, string name, string avatar, string token, string role, string userId)
        {
            try
            {
                var authenProps = new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                };

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
                identity.AddClaim(new Claim(ClaimTypes.Name, name));
                identity.AddClaim(new Claim(ClaimTypes.Email, email));
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                identity.AddClaim(new Claim(ClaimTypes.Authentication, token));
                identity.AddClaim(new Claim(Avatar, avatar));
                
                await Current.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Update user claim
        /// </summary>
        /// <param name="curentPrincipal">claim current</param>
        /// <param name="name">name user</param>
        /// <returns>Update user claim</returns>
        public async Task<bool> UpdateUserClaim(ClaimsIdentity identity, string name, string avatar)
        {
            try
            {
                if (identity == null) return false;

                var existingIdentityName = identity.FindFirst(claim => claim.Type == ClaimTypes.Name);
                if(existingIdentityName != null)
                {
                    identity.RemoveClaim(existingIdentityName);
                    identity.AddClaim(new Claim(ClaimTypes.Name, name));
                }

                var exstingIdentityAvatar = identity.FindFirst(claim => claim.Type == Avatar);
                if(exstingIdentityAvatar != null)
                {
                    identity.RemoveClaim(exstingIdentityAvatar);
                    identity.AddClaim(new Claim(Avatar, avatar));
                }

                await Current.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sign out cookies
        /// </summary>
        public void SignOut()
        {
            Current.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        /// <summary>
        /// Get claim
        /// </summary>
        /// <param name="key">key object</param>
        /// <returns></returns>
        public string GetClaim(string key)
        {
            try
            {
                var result = new List<string>();
                foreach(var claim in Current.User.Claims)
                {
                    if (claim.Type.Equals(key))
                    {
                        result.Add(claim.Value);
                    }
                }
                return string.Join(";", result);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// Get user name
        /// </summary>
        public string UserName
        {
            get
            {
                return Current.User.Identity.Name;
            }
        }
        /// <summary>
        /// Get user id
        /// </summary>
        public string UserId
        {
            get
            {
                return Current.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
        /// <summary>
        /// Get role user
        /// </summary>
        public string RoleName
        {
            get
            {
                return Current.User.FindFirst(ClaimTypes.Role).Value;
            }
        }

        /// <summary>
        /// Get Avatar
        /// </summary>
        public string UserAvt
        {
            get
            {
                return Current.User.FindFirst(Avatar).Value;
            }
        }
        public HttpContext Current
        {
            get
            {
                return _httpContextAccessor.HttpContext;
            }
        }
    }
}
