using System;
using System.Text;
using Repositories;
using GroupStudyAPI.Models;
using BusinessObject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace GroupStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public AuthController(IUserRepository userRepository, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _userRepository = userRepository;
            _appSettings = optionsMonitor.CurrentValue;
        }

        // Existing code...

        [HttpPost("Register")]
        public IActionResult Register(RegisterModel model)
        {
            var existingUser = _userRepository.GetUserByEmail(model.Email);
            if (existingUser != null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Email already registered"
                });
            }

            var user = new User
            {
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            // Set the UserId as auto-incremented
            user.UserId = _userRepository.GetNextUserId();

            // Save the new user to the repository
            _userRepository.SaveUser(user);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Registration successful"
            });
        }

        [HttpPost("Login")]
        public IActionResult Validate(AuthModel model)
        {
            var user = _userRepository.GetUserByEmail(model.Email);
            if (user == null || user.Password != model.Password)
            {
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Invalid username/password"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = GenerateToken(user)
            });
        }

        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.LastName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }






    }
}
