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

        // Kullanıcı bilgilerini almak için endpoint
        [HttpGet("user")]
        [Authorize] // Kimlik doğrulaması yapılması gerektiğini belirtiyoruz
        public async Task<IActionResult> GetUserInfo()
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();  // Eğer kimlik doğrulama yapılmamışsa 401 döndürüyoruz
            }

            // Try to convert the userId from string to int
            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized(); // If conversion fails, return unauthorized
            }

            var user = await _userService.GetUserByIdAsync(userId);  // Pass the integer userId

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return Ok(user);

        }

    }
}
