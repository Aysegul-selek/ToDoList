using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoAPI.Business.Abstract;
using ToDoAPI.Business.Concrete;
using ToDoAPI.Entities.DTOs;
using ToDoAPI.Entities.DTOs.Auth;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            await _userService.RegisterAsync(registerDto);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _userService.LoginAsync(loginDto);
            return Ok(response);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByUsernameAsync(username);  // <-- BU methodu userService içinde yazmalısın

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return Ok(user);
        }


    }
}
