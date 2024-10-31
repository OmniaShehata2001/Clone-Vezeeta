using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.UserServices;
using Vezeeta.Dtos.DTOs.AuthDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userServices) 
        {
            _userService = userServices;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {

            if (ModelState.IsValid)
            {
                var result = await _userService.RegistersUserAsync(userDto);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.loginUserAsync(userDto);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.GetAllUsersAsync();
                if (result.Entities is not null)
                {
                    return Ok(result.Entities);
                }
                return BadRequest(result);
            }

            return BadRequest(ModelState);

        }
        [HttpGet("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            if (ModelState.IsValid)
            {
                await _userService.LogOut();
                return Ok("LogOut Successfully");
            }

            return BadRequest(ModelState);
        }
    }
}
