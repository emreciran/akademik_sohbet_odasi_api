using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserManagerResponse> ConfirmEmail(string userID, string token)
        {
            return await _userRepository.ConfirmEmail(userID, token);
        }

        public async Task<UserManagerResponse> ForgetPassword(string email)
        {
            return await _userRepository.ForgetPassword(email);
        }

        public async Task<UserManagerResponse> LoginUser(LoginViewModel model)
        {
            return await _userRepository.LoginUser(model);
        }

        public async Task<UserManagerResponse> RefreshToken(TokenRequest tokenRequest)
        {
            return await _userRepository.RefreshToken(tokenRequest);
        }

        public async Task<UserManagerResponse> RegisterUser(RegisterViewModel model)
        {
            return await _userRepository.RegisterUser(model);
        }

        public async Task<UserManagerResponse> ResetPassword(ResetPasswordViewModel model)
        {
            return await _userRepository.ResetPassword(model);
        }
    }
}
