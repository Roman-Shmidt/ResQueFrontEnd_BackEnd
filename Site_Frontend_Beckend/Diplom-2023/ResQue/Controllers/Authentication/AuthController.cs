using Infrastructure.FunctionalStyleResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ResQue.Controllers.Common;
using ResQueBackEnd.Managers.ClientDomain;
using ResQueBackEnd.Managers.UserDomain;
using ResQueDal.UserDomain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ResQue.Controllers.Authentication
{
    [Route("token")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserManager _userManager;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public AuthController(IConfiguration configuration,
            IUserManager userManager)
        {
            _configuration = configuration;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost()]
        public async Task<IActionResult> GenerateToken([FromBody] UserLoginModel userModel)
        {
            var maybeUser = await _userManager.GetUserByEmail(userModel.Email, userModel.Password);

            if (maybeUser.HasValue && maybeUser.Value is UserDto user) 
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("UserType", user.UserType.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest("Invalid credentials");
        }        
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
