using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.RoleServices;
using Vezeeta.Dtos.DTOs.AuthDtos;
using Vezeeta.Dtos.DTOs.RoleDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleServices.CreateRoleAsync(roleDto);
                if (result.Succeeded)
                {
                    return Ok("Successfully");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating role.");
                }
            }
            return BadRequest();
        }
        [HttpPost("RegisterWithRole")]
        public async Task<IActionResult> CreateUserWithRole(RegisterDto registerDto, string roleName)
        {
            var result = await _roleServices.CreateUserAsync(registerDto.UserName, registerDto.Email, registerDto.Password, roleName);
            if (result.Succeeded)
            {
                return Ok("Successfully");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest();
        }
    }
}
