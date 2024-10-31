using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOs.RoleDtos;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.RoleServices
{
    public interface IRoleServices
    {
        Task<IdentityResult> CreateRoleAsync(RoleDto roleDto);
        Task<List<string>> GetRolesAsync();
        Task<IdentityResult> CreateUserAsync(string userName, string email, string password, string roleName);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName);
    }
}
