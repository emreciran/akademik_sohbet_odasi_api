using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class UserRepository : IUserRepository
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;
        private IMailRepository _mailRepository;
        private ApplicationDbContext _applicationDbContext;
        private TokenValidationParameters _tokenValidationParams;

        public UserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMailRepository mailRepository, ApplicationDbContext applicationDbContext, TokenValidationParameters tokenValidationParams)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mailRepository = mailRepository;
            _applicationDbContext = applicationDbContext;
            _tokenValidationParams = tokenValidationParams;
        }

        public async Task<UserManagerResponse> ConfirmEmail(string userid, string token)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "Kullanıcı bulunamadı!",
                    IsSuccess = false
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email başarıyla onaylandı.",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Email onaylanmadı!",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Girdiğiniz email adresine ait kullanıcı bulunamadı!",
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppURL"]}/ResetPassword?email={email}&token={validToken}";

            string subject = "Şifre Yenileme";
            string content = $"<h1>Akademik Sohbet Odası</h1>" +
                $"<p>Şifrenizi sıfırlamak için <a href='{url}'>Tıklayınız</a></p>";

            await _mailRepository.SendEmailAsync(email, subject, content);

            return new UserManagerResponse
            {
                Message = "Şifre yenileme email adresinize gönderildi.",
                IsSuccess = true
            };
        }

        public async Task<UserManagerResponse> LoginUser(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "Girdiğiniz email adresine ait hesap bulunamadı!",
                    IsSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return new UserManagerResponse
                {
                    Message = "Şifreniz Hatalı!",
                    IsSuccess = false
                };
            }

            var userRole = await _userManager.GetRolesAsync(user);

            var jwtToken = await GenerateJwtToken(user);

            var authresult = new AuthResult
            {
                Token = jwtToken.Token,
                RefreshToken = jwtToken.RefreshToken,
                Success = jwtToken.Success,
                Errors = jwtToken.Errors
            };

            return new UserManagerResponse
            {
                Message = "",
                Role = userRole,
                IsSuccess = true,
                AuthResult = authresult
            };
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var userRoles = await _userManager.GetRolesAsync(user);
            var errors = new List<string>();

            var userDetails = _applicationDbContext.Users.FirstOrDefault(x => x.Username == email);

            if(user == null)
            {
                errors.Add("Kullanıcı bulunamadı!");
                return null;
            }

            return new User
            {
                Email = userDetails.Email,
                Username = userDetails.Username,
                Name = userDetails.Name,
                Surname = userDetails.Surname,
                Role = userDetails.Role,
                CreatedDate = userDetails.CreatedDate,
                UserProjects = userDetails.UserProjects,
                User_ID = userDetails.User_ID
            };
        }

        public async Task<UserManagerResponse> RegisterUser(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null!");

            var existingUserEmail = await _userManager.FindByEmailAsync(model.Email);
            var existingUserName = await _userManager.FindByNameAsync(model.Username);

            if (existingUserEmail != null)
            {
                return new UserManagerResponse
                {
                    Message = "Bu email zaten kullanılıyor!",
                    IsSuccess = false
                };
            }

            if (existingUserName != null)
            {
                return new UserManagerResponse
                {
                    Message = "Bu kullanıcı adı zaten kullanılıyor!",
                    IsSuccess = false
                };
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Şifreniz onay şifresi ile eşleşmiyor!",
                    IsSuccess = false
                };
            }

            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Username
            };

            var userDetails = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Username = model.Username,
                Role = "Ogrenci",
                CreatedDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Ogrenci");

                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppURL"]}/api/auth/confirmemail?userid={user.Id}&token={validEmailToken}";

                string subject = "E postanızı onaylayın";
                string content = $"<h1>Akademik Sohbet Odası</h1>" +
                    $"<p>E postanızı onaylamak için lütfen <a href='{url}'>Tıklayınız</a></p>";

                await _mailRepository.SendEmailAsync(user.Email, subject, content);

                var jwtToken = await GenerateJwtToken(user);

                await _applicationDbContext.Users.AddAsync(userDetails);
                await _applicationDbContext.SaveChangesAsync();

                var authresult = new AuthResult
                {
                    Token = jwtToken.Token,
                    RefreshToken = jwtToken.RefreshToken,
                    Success = jwtToken.Success,
                    Errors = jwtToken.Errors
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                return new UserManagerResponse
                {
                    Message = "Kullanıcı başarıyla oluşturuldu.",
                    Role = userRoles,
                    IsSuccess = true,
                    AuthResult = authresult
                };
            }

            return new UserManagerResponse
            {
                Message = "Kullanıcı oluşturulamadı!",
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }

        public async Task<UserManagerResponse> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "Girdiğiniz email adresine ait kullanıcı bulunamadı!",
                    IsSuccess = false
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Şifreniz onay şifresi ile eşleşmiyor!",
                    IsSuccess = false
                };

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Şifreniz başarıyla değiştirildi",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                Message = "Hata!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }

        public async Task<UserManagerResponse> RefreshToken(TokenRequest tokenRequest)
        {
            if (tokenRequest == null)
            {
                return new UserManagerResponse
                {
                    Message = "Null error",
                    IsSuccess = false
                };
            }

            var existRefreshToken = await _applicationDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

            if (existRefreshToken == null)
            {
                return new UserManagerResponse
                {
                    Message = "Refresh token bulunamadı!",
                    IsSuccess = false
                };
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            var userRole = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "Kullanıcı bulunamadı!",
                    IsSuccess = false
                };
            }

            // update current token 
            var storedToken = await _applicationDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);
            storedToken.IsUsed = true;
            _applicationDbContext.RefreshTokens.Update(storedToken);
            await _applicationDbContext.SaveChangesAsync();

            var result = await GenerateJwtToken(user);

            if (!result.Success)
            {
                return new UserManagerResponse
                {
                    AuthResult = result,
                    IsSuccess = false,
                    Errors = result.Errors,
                };
            }

            if (result == null)
            {
                return new UserManagerResponse
                {
                    Message = "Geçersiz token",
                    IsSuccess = false
                };
            }

            return new UserManagerResponse
            {
                Message = "",
                IsSuccess = true,
                AuthResult = result,
                Role = userRole
            };
        }

        private async Task<AuthResult> GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var claims = await GetAllValidClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevorked = false,
                UserId = user.Id,
                AddedDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMonths(6),
                Token = RandomString(35) + Guid.NewGuid()
            };

            await _applicationDbContext.RefreshTokens.AddAsync(refreshToken);
            await _applicationDbContext.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<List<Claim>> GetAllValidClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }

        //private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();
            
        //    try
        //    {
        //        // Validation 1 - Validation JWT token format
        //        var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out var validatedToken);

        //        // Validation 2 - Validate encryption alg
        //        if (validatedToken is JwtSecurityToken jwtSecurityToken)
        //        {
        //            var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        //            if (result == false)
        //            {
        //                return null;
        //            }
        //        }

        //        // Validation 3 - validate expiry date
        //        var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        //        var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

        //        if (expiryDate > DateTime.Now)
        //        {
        //            return new AuthResult()
        //            {
        //                Success = false,
        //                Errors = new List<string>() {
        //                    "Token has not yet expired"
        //                }
        //            };
        //        }

        //        // validation 4 - validate existence of the token
        //        var storedToken = await _applicationDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

        //        if (storedToken == null)
        //        {
        //            return new AuthResult()
        //            {
        //                Success = false,
        //                Errors = new List<string>() {
        //                    "Token does not exist"
        //                }
        //            };
        //        }

        //        // Validation 5 - validate if used
        //        if (storedToken.IsUsed)
        //        {
        //            return new AuthResult()
        //            {
        //                Success = false,
        //                Errors = new List<string>() {
        //                    "Token has been used"
        //                }
        //            };
        //        }

        //        // Validation 6 - validate if revoked
        //        if (storedToken.IsRevorked)
        //        {
        //            return new AuthResult()
        //            {
        //                Success = false,
        //                Errors = new List<string>() {
        //                    "Token has been revoked"
        //                }
        //            };
        //        }

        //        // Validation 7 - validate the id
        //        var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        //        if (storedToken.JwtId != jti)
        //        {
        //            return new AuthResult()
        //            {
        //                Success = false,
        //                Errors = new List<string>() {
        //                    "Token doesn't match"
        //                }
        //            };
        //        }

        //        // update current token 

        //        storedToken.IsUsed = true;
        //        _applicationDbContext.RefreshTokens.Update(storedToken);
        //        await _applicationDbContext.SaveChangesAsync();

        //        // Generate a new token
        //        var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
        //        return await GenerateJwtToken(dbUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("Lifetime validation failed. The token is expired."))
        //        {

        //            return new AuthResult()
        //            {
        //                Success = false,
        //                Errors = new List<string>() {
        //                    "Token has expired please re-login"
        //                }
        //            };

        //        }
        //        else
        //        {
        //            return new AuthResult()
        //            {
        //                Success = false,
        //                Errors = new List<string>() {
        //                    "Something went wrong."
        //                }
        //            };
        //        }
        //    }
        //}

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

    }
}
