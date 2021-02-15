using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBTracker.Contracts.ViewModels;
using BBTracker.Web.Settings;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BBTracker.App;
using Microsoft.AspNetCore.Authorization;

namespace BBTracker.Web.Controllers
{
    [Route("[controller]")]
    //[Authorize(Roles = "Admin,User")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserViewModel createUserViewModel)
        {
            if (await _userService.CreateUser(createUserViewModel))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            var response = await _userService.Login(loginViewModel);
            if (!response)
            {
                return BadRequest();
            }
            

            var jwtSettings = new JwtSettings();

            var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, loginViewModel.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddSeconds(jwtSettings.LifetimeInSeconds),
                Issuer = jwtSettings.ValidIssuer,
                Audience = loginViewModel.Audience,
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return Ok
                (
                new { access_token = tokenHandler.WriteToken(token) }
                );
        }
    }
}
