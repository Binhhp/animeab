using AnimeAB.Application.Behavious;
using AnimeAB.Domain;
using AnimeAB.Domain.DTOs;
using AnimeAB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Application.Reponsitories
{
    public interface IReponsitoryAccount
    {
        /// <summary>
        /// Create account with email and password
        /// </summary>
        /// <param name="account">Infor email password fullname, ....</param>
        /// <returns></returns>
        Response CreateEmailPasswordAsync(AccountSignUpDto account);
        /// <summary>
        /// Signin with email and password
        /// </summary>
        /// <param name="account">infor account: email vs password</param>
        /// <param name="isClient">check client</param>
        /// <returns></returns>
        Task<Response> SignInEmailPasswordAsync(AccountLoginDto account, bool isClient = false);
        Task<IEnumerable<AnimeUser>> GetUsersAsync();
        /// <summary>
        /// Get infor user loggined with token
        /// </summary>
        /// <param name="firebaseToken"></param>
        /// <returns></returns>
        Task<ClientProfile> GetUserCurrentAsync(string firebaseToken);
        /// <summary>
        /// Get infor user current from db context
        /// </summary>
        /// <param name="uid">user uid</param>
        /// <returns></returns>
        Task<AnimeUser> GetUserContextAsync(string uid);
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="firebaseToken">token user logined</param>
        /// <param name="userId">user uid</param>
        /// <returns></returns>
        Task<bool> DeleteUserAsync(string firebaseToken, string userId);
        /// <summary>
        /// Update profile user current loggined
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        Task<Response> UpdateProfileAsync(ProfileRequest profile);
        /// <summary>
        /// Get password with feature forgot pass
        /// </summary>
        /// <param name="email">Email get pw</param>
        /// <returns></returns>
        Response ForgotPasswordAsync(string email);
        /// <summary>
        /// Change pw
        /// </summary>
        /// <param name="idToken">token user logined</param>
        /// <param name="email">email of user</param>
        /// <param name="password">password using logined</param>
        /// <param name="newPassword">password new changed</param>
        /// <returns></returns>
        Task<Response> ChangePasswordAsync(string idToken, string email, string password, string newPassword);
    }
}
