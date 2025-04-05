using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Business.Concrete;
using ToDoAPI.Core;
using ToDoAPI.Data.IRepositories;
using ToDoAPI.Entities.Auth;
using ToDoAPI.Entities.DTOs;
using ToDoAPI.Entities.DTOs.Auth;

namespace ToDoAPI.Business.Abstract
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async  Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            return new UserDto
            {
                UserName = user.Username,
                Email = user.Email
            };

        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.Username,
                Email = user.Email
                // diğer gerekli alanlar
            };
        }


        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null) throw new Exception("User not found.");

          
            var hashedPassword = PasswordHelper.HashPassword(loginDto.Password);
            if (user.PasswordHash != hashedPassword) throw new Exception("Invalid password.");

         
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }; 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async  Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(registerDto.Username);
            if (existingUser != null) throw new Exception("User already exists.");
           
            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = PasswordHelper.HashPassword(registerDto.Password);

            await _userRepository.AddAsync(user);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
