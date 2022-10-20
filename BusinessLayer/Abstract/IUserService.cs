using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUser(RegisterViewModel model);

        Task<UserManagerResponse> LoginUser(LoginViewModel model);

        Task<UserManagerResponse> ConfirmEmail(string userID, string token);

        Task<UserManagerResponse> ForgetPassword(string email);

        Task<UserManagerResponse> ResetPassword(ResetPasswordViewModel model);

        Task<UserManagerResponse> RefreshToken(TokenRequest tokenRequest);

    }
}
