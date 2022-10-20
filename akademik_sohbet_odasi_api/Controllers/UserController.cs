using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akademik_sohbet_odasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Ogrenci, Egitimci")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        private IRoleService _roleService;

        public UserController(IUserRepository userRepository, IRoleService roleService)
        {
            _userRepository = userRepository;
            _roleService = roleService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUserInfoFromLogined()
        {
            var response = await _userRepository.GetUserByEmail(HttpContext.User.Identity.Name);
            return Ok(response);
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _roleService.GetAllUsers();
            return Ok(users);
        }
    }
}
