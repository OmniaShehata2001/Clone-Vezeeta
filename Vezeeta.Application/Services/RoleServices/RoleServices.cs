using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOs.RoleDtos;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.RoleServices
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleServices(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> CreateRoleAsync(RoleDto roleDto)
        {
            var role = new IdentityRole(roleDto.RoleName);
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> CreateUserAsync(string userName, string email, string password, string roleName)
        {
            var user = new ApplicationUser { UserName = userName, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await _userManager.AddToRoleAsync(user, roleName);
            }

            return result;
        }

        public async Task<List<string>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return roles;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
