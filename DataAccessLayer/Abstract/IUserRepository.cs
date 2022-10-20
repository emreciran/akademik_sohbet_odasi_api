using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUserRepository
    {
        Task<UserManagerResponse> RegisterUser(RegisterViewModel model);

        Task<UserManagerResponse> LoginUser(LoginViewModel model);

        Task<UserManagerResponse> ConfirmEmail(string userID, string token);

        Task<UserManagerResponse> ForgetPassword(string email);

        Task<UserManagerResponse> ResetPassword(ResetPasswordViewModel model);

        Task<UserManagerResponse> RefreshToken(TokenRequest tokenRequest);

        Task<User> GetUserByEmail(string email);
    }
}
