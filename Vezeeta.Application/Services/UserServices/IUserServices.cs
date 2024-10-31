using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOs.AuthDtos;
using Vezeeta.Dtos.Result;

namespace Vezeeta.Application.Services.UserServices
{
    public interface IUserServices
    {
        Task<ResultView<UserDto>> RegistersUserAsync(UserDto userDto);
        Task<ResultView<UserTokenDto>> loginUserAsync(LoginDto userDto);
        Task<ResultDataList<UserDto>> GetAllUsersAsync();
        Task LogOut();
    }
}
