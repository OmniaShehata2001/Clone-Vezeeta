using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.UserRepository;
using Vezeeta.Dtos.DTOs.AuthDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserServices(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
        }

        public async Task<ResultDataList<UserDto>> GetAllUsersAsync()
        {
            var Users = (await _userRepository.GetAllasync()).ToList();
            if (Users == null)
            {
                return new ResultDataList<UserDto>
                {
                    Entities = null,
                    Count = 0
                };
            }

            return new ResultDataList<UserDto>
            {
                Entities = _mapper.Map<List<UserDto>>(Users),
                Count = Users.Count()
            };
        }

        public async Task<ResultView<UserTokenDto>> loginUserAsync(LoginDto userDto)
        {
            var ExistingUser = await _userManager.FindByEmailAsync(userDto.Email);

            if (ExistingUser is null)
            {
                return new ResultView<UserTokenDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Email isn't Exist"
                };
            }
            else
            {
                var ExistingPassword = await _userManager.CheckPasswordAsync(ExistingUser, userDto.Password);
                if (ExistingPassword)
                {
                    var Claims = new List<Claim>
                     {
                        new Claim("Email", ExistingUser.Email),
                        new Claim("UserId", ExistingUser.Id.ToString()), 
                        new Claim("UserName", ExistingUser.UserName), 
                        new Claim("PhoneNumber", ExistingUser.PhoneNumber), 
                        new Claim("Gender", ExistingUser.Gender.ToString()) 
                     };

                    SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));

                    JwtSecurityToken token = new JwtSecurityToken(
                       issuer: _config["JWT:Issuer"],
                       audience: _config["JWT:Audiance"],
                       claims: Claims,
                       expires: DateTime.Now.AddDays(3),
                       signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                     );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    var UserToken = new UserTokenDto
                    {
                        Id = ExistingUser.Id,
                        Email = ExistingUser.Email,
                        token = tokenString,
                    };
                    var result = await _signInManager.PasswordSignInAsync(userDto.Email, userDto.Password,false, lockoutOnFailure: false);

                    return new ResultView<UserTokenDto>
                    {
                        Entity = UserToken,
                        IsSuccess = true,
                        Message = "Login successful"
                    };
                }
                else
                {
                    return new ResultView<UserTokenDto>
                    {
                        Entity = null,
                        IsSuccess = false,
                        Message = "Password Isn't Correct"//password
                    };
                }
            }

        }

        public async Task<ResultView<UserDto>> RegistersUserAsync(UserDto userDto)
        {
            var ExistingUser = await _userRepository.GetByEmailAsync(userDto.Email);
            if (ExistingUser is not null)
            {
                return new ResultView<UserDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "User Is Already Exist"
                };
            }

            var user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                BithDay = userDto.BirthDate,
                Gender = userDto.Gender,
            };
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                return new ResultView<UserDto>
                {
                    Entity = _mapper.Map<UserDto>(user),
                    IsSuccess = true,
                    Message = "User Register Successfully"
                };
            }

            return new ResultView<UserDto>
            {
                Entity = null,
                IsSuccess = false,
                Message = "Faild To Register"
            };
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
         }
    }
}
