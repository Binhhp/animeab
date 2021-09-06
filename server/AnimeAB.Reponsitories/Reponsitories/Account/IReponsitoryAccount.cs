using AnimeAB.Reponsitories.Domain;
using AnimeAB.Reponsitories.DTO;
using AnimeAB.Reponsitories.Entities;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.Account
{
    public interface IReponsitoryAccount
    {
        Response CreateEmailPasswordAsync(AccountSignUpDto account);
        Task<Response> SignInEmailPasswordAsync(AccountLoginDto account, bool isClient = false);
        Task<IEnumerable<AnimeUser>> GetUsersAsync();
        Task<ClientProfile> GetUserCurrentAsync(string firebaseToken);
        Task<bool> DeleteUserAsync(string firebaseToken, string userId);
        Task<Response> UpdateProfileAsync(ProfileDomain profile);
        Response ForgotPasswordAsync(string email);
        Task<Response> ChangePasswordAsync(string idToken, string email, string password, string newPassword);
    }
}
