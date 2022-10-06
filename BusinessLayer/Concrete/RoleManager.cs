using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Identity;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RoleManager : IRoleService
    {
        IRoleRepository _roleRepository;

        public RoleManager(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleManagerResponse> AddUserToRole(string email, string roleName)
        {
            return await _roleRepository.AddUserToRole(email, roleName);
        }

        public async Task<RoleManagerResponse> CreateRole(string name)
        {
            return await _roleRepository.CreateRole(name);
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            return await _roleRepository.GetAllRoles();
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            return await _roleRepository.GetAllUsers();
        }

        public async Task<IList<string>> GetUserRoles(string email)
        {
            return await _roleRepository.GetUserRoles(email);
        }

        public async Task<RoleManagerResponse> RemoveUserFromRole(string email, string roleName)
        {
            return await _roleRepository.RemoveUserFromRole(email, roleName);
        }

        public async Task<RoleManagerResponse> UpdateUserFromRole(string email, string currentRoleName, string newRoleName)
        {
            return await _roleRepository.UpdateUserFromRole(email, currentRoleName, newRoleName);
        }
    }
}
