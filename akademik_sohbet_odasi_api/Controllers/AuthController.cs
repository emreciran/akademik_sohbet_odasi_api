using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace akademik_sohbet_odasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private IUserRepository _userRepository;
        private IConfiguration _configuration;
        private ApplicationDbContext _applicationDbContext;

        public AuthController(UserManager<IdentityUser> userManager, IUserRepository userRepository, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.RegisterUser(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Bazı değerler uygun değil!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.LoginUser(model);

                if (result.IsSuccess)
                {
                    var jwt = result.AuthResult.Token;
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    HttpContext.Response.Cookies.Append("jwt", jwt, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Bazı değerler uygun değil!");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userid, [FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await _userRepository.ConfirmEmail(userid, token);

            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["AppURL"]}/akademik_sohbet_odasi_api/ConfirmEmail.html");
            }

            return BadRequest(result);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }

            var result = await _userRepository.ForgetPassword(email);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm]ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.ResetPassword(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.RefreshToken(tokenRequest);

                if (result.IsSuccess)
                {
                    var jwt = result.AuthResult.Token;
                    HttpContext.Response.Cookies.Append("jwt", jwt, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.Now.AddDays(1)
                    });
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest();
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("Success");
        }
    }
}
