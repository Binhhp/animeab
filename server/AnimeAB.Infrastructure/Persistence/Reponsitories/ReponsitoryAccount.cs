using AnimeAB.Domain.DTOs;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeAB.Domain.Entities;
using FireSharp.Interfaces;
using FireSharp;
using Newtonsoft.Json;
using Firebase.Storage;
using System.IO;
using System.Threading;
using AnimeAB.Domain;
using System.Net.Http;
using AnimeAB.Application.Common.Behaviour;
using AnimeAB.Application.Common.Interface.Reponsitories;
using AnimeAB.Domain.Settings;
using AnimeAB.Domain.ValueObject;
using AnimeAB.Infrastructure.Services;

namespace AnimeAB.Infrastructure.Persistence.Reponsitories
{
    public class ReponsitoryAccount : Request, IReponsitoryAccount
    {
        private readonly FirebaseAuthProvider authProvider;
        private readonly IFirebaseClient database;
        private readonly FirebaseStorage storage;

        public ReponsitoryAccount(AppSettingFirebase appSetting)
        {
            authProvider = FirebaseManager.Authenticate(appSetting.ApiKey);
            database = FirebaseManager.Database(appSetting.AuthSecret, appSetting.DatabaseURL);
            storage = FirebaseManager.Storage(appSetting.StorageBucket);
        }
        /// <summary>
        /// Create user with email password
        /// </summary>
        /// <param name="account">email password fullname</param>
        /// <returns> Create user with email password</returns>
        public Response CreateEmailPasswordAsync(AccountSignUpDto account)
        {
            try
            {

                var result = Task.Run(() => authProvider.CreateUserWithEmailAndPasswordAsync(account.Email,
                account.Password, account.FullName, true));

                if (result == null) return Error("Error Interval Server");

                var userCreated = result.Result.User;
                string token = result.Result.FirebaseToken;

                var user = new AnimeUser
                {
                    LocalId = userCreated.LocalId,
                    Email = userCreated.Email,
                    DisplayName = userCreated.DisplayName,
                    IsEmailVerified = userCreated.IsEmailVerified,
                    Role = account.Role,
                    PhotoUrl = userCreated.PhotoUrl
                };

                Task.Run(() => database.SetAsync(Table.USERS + "/" + userCreated.LocalId, user));
                return Success(user, "Bạn cần xác nhận email để hoàn thành đăng ký nhé!");
            }
            catch(Exception ex)
            {
                if(ex.Message.ToString().IndexOf("EMAIL_EXISTS") > -1)
                {
                    return Error("Email đã tồn tại.");
                }
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// notify child function
        /// </summary>
        /// <param name="localId"></param>
        /// <returns></returns>
        private void NotifySuccessRegister(string localId)
        {
            var notification = new Notification
            {
                Key = "notify-1",
                LinkNotify = "/",
                Message = "Chào mừng bạn đến với AnimeAB. Nơi thưởng thức những bộ phim Anime, Manga Nhật Bản mới nhất, hay nhất, vietsub ##",
                Title = "📣 WELCOME!!",
                UserRevice = localId
            };

            database.SetAsync(Table.NOTIFICATION + "/" + localId + "/" + notification.Key, notification);
        }
        /// <summary>
        /// update email child function
        /// </summary>
        /// <param name="localId"></param>
        /// <param name="isEmail"></param>
        /// <returns></returns>
        private void UpdateEmail(string localId, bool isEmail)
        {
            database.SetAsync(Table.USERS + "/" + localId + "/IsEmailVerified", isEmail);
        }
        /// <summary>
        /// Sign in email password
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Sign in email password</returns>
        public async Task<Response> SignInEmailPasswordAsync(AccountLoginDto account, bool isClient = false)
        {
            try
            {
                var result = await authProvider.SignInWithEmailAndPasswordAsync(account.Email, account.Password);

                if (result == null)
                {
                    return Error("Oh No! Email không tồn tại! Hãy đăng ký!");
                }

                var userLoggined = result.User;

                if (!userLoggined.IsEmailVerified) return Error("Tài khoản chưa xác nhận email đăng ký! Hãy đi tới email của bạn trước khi đăng nhâp");

                var dataQuery = await database.GetAsync(Table.USERS + "/" + result.User.LocalId);
                var userQuery = dataQuery.ResultAs<ProfileDto>();

                if (userQuery.IsEmailVerified != userLoggined.IsEmailVerified)
                {
                    Parallel.Invoke(
                        () => UpdateEmail(userLoggined.LocalId, userLoggined.IsEmailVerified), 
                        () =>  NotifySuccessRegister(userLoggined.LocalId));
                }

                if (isClient)
                {
                    var client = new ClientToken
                    {
                        localId = userLoggined.LocalId,
                        token = result.FirebaseToken,
                        refresh_token = result.RefreshToken
                    };

                    return Success(client);
                }
                else
                {
                    var userProfile = new ProfileDto
                    {
                        LocalId = userLoggined.LocalId,
                        DisplayName = userLoggined.DisplayName,
                        Email = userLoggined.Email,
                        IsEmailVerified = userLoggined.IsEmailVerified,
                        PhotoUrl = userLoggined.PhotoUrl,
                        Token = result.FirebaseToken,
                        Role = userQuery.Role
                    };
                    return Success(userProfile);
                }

            }
            catch(Exception ex)
            {
                if(ex.Message.IndexOf("INVALID_PASSWORD") > -1)
                {
                    return Error("Bạn nhập sai mật khẩu rồi!");
                }
                if(ex.Message.IndexOf("EMAIL_NOT_FOUND") > -1)
                {
                    return Error("Oh No! Email không tồn tại! Hãy đăng ký!");
                }
                if(ex.Message.IndexOf("TOO_MANY_ATTEMPTS_TRY_LATER") > -1)
                {
                    return Error("Quyền truy cập vào tài khoản này đã tạm thời bị vô hiệu hóa do nhiều lần đăng nhập không thành công. Bạn có thể khôi phục nó ngay lập tức bằng cách đặt lại mật khẩu của mình hoặc bạn có thể thử lại sau.");
                }
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// Get user current
        /// </summary>
        /// <param name="firebaseToken"></param>
        /// <returns></returns>
        public async Task<ClientProfile> GetUserCurrentAsync(string firebaseToken)
        {
            try
            {
                User result = await authProvider.GetUserAsync(firebaseToken);
                
                var user = new ClientProfile
                {
                    name = result.DisplayName,
                    email = result.Email,
                    photo_url = result.PhotoUrl
                };
                return user;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AnimeUser>> GetUsersAsync()
        {
            try
            {
                var data = await database.GetAsync(Table.USERS);
                if (data.Body == "null") return null;

                Dictionary<string, AnimeUser> users = data.ResultAs<Dictionary<string, AnimeUser>>();

                return users.Values.ToList();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="firebaseToken">firebase token</param>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAsync(string firebaseToken, string userId)
        {
            try
            {
                var deleteUser = Task.Run(() => database.DeleteAsync(Table.USERS + "/" + userId));
                var deleteAuth = Task.Run(() => authProvider.DeleteUserAsync(firebaseToken));
                await Task.WhenAll(deleteUser, deleteAuth);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="firebaseToken"></param>
        /// <param name="displayName"></param>
        /// <param name="photo"></param>
        /// // <param name="fileName"></param>
        /// <returns></returns>
        public async Task<Response> UpdateProfileAsync
            (ProfileDomain profile)
        {
            try
            {
                string photoURL = profile.Avatar;

                if(profile.StreamAvatar != null && profile.StreamAvatar.Length > 0)
                {
                    var cancellation = new CancellationTokenSource();

                    await storage.Child(Table.USERS).Child(profile.FileName).PutAsync(profile.StreamAvatar, cancellation.Token);

                    photoURL = await storage.Child(Table.USERS).Child(profile.FileName).GetDownloadUrlAsync();
                }

                var firebaseAuth = await authProvider.UpdateProfileAsync(profile.Token, profile.DisplayName, photoURL);

                var userUpdated = firebaseAuth.User;
                var user = new Client
                {
                    Email = userUpdated.Email,
                    DisplayName = userUpdated.DisplayName,
                    IsEmailVerified = userUpdated.IsEmailVerified,
                    PhotoUrl = photoURL,
                    LocalId = userUpdated.LocalId
                };

                if (!string.IsNullOrWhiteSpace(profile.Email))
                {
                    var emailValided = await authProvider.GetUserAsync(profile.Token);
                    if (emailValided.Email != profile.Email)
                    {
                        var checkEmail = await authProvider.GetLinkedAccountsAsync(profile.Email);
                        if (checkEmail.IsRegistered)
                        {
                            return Error("Email đã được sử dụng! Bạn cần thay đổi email mới!");
                        }
                        var changeEmail = await authProvider.ChangeUserEmail(profile.Token, profile.Email);
                        await authProvider.SendEmailVerificationAsync(changeEmail.FirebaseToken);
                        user.Email = changeEmail.User.Email;
                        user.IsEmailVerified = false;
                    }
                }

                await database.UpdateAsync(Table.USERS + "/" + userUpdated.LocalId, user);
                return Success(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Response ForgotPasswordAsync(string email)
        {
            try
            {
                var checkEmail = (authProvider.GetLinkedAccountsAsync(email)).Result;
                if (!checkEmail.IsRegistered) return Error("Email không tồn tại! Hãy đăng ký!");

                Task.Run(() => authProvider.SendPasswordResetEmailAsync(email));
                return Success();

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="idToken"></param>
        /// <param name="password"></param>
        public async Task<Response> ChangePasswordAsync(string idToken, string email, string password, string newPassword)
        {
            try
            {
                try
                {
                    var checkLogin = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                    if(checkLogin == null) return Error("Sai mật khẩu rồi.");
                }
                catch
                {
                    return Error("Sai mật khẩu rồi.");
                }

                var result = await authProvider.ChangeUserPassword(idToken, newPassword);
                var client = new ClientToken();
                if(result != null)
                {
                    client.localId = result.User.LocalId;
                    client.token = result.FirebaseToken;
                    client.refresh_token = result.RefreshToken;
                }
                return Success(client);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    public class UpdateProfile
    {
        public string? DisplayName { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
