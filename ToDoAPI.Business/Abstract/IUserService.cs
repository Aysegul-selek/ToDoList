using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.DTOs;
using ToDoAPI.Entities.DTOs.Auth;

namespace ToDoAPI.Business.Concrete
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetUserByUsernameAsync(string username);

    }
}
