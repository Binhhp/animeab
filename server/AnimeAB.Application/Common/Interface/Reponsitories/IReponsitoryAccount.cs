using AnimeAB.Application.Common.Behaviour;
using AnimeAB.Domain;
using AnimeAB.Domain.DTOs;
using AnimeAB.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimeAB.Application.Common.Interface.Reponsitories
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
