using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InficareSushan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InficareSushan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;


        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userlogin)
        {
            var user = Authenticate(userlogin);

            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("user not found");
        }

        private string GenerateToken(UserModel user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.username),
                new Claim(ClaimTypes.Role, user.role)

            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
               _config["Jwt:Audience"],
               claims,
               expires: DateTime.Now.AddMinutes(15),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLogin userlogin)
        {
            var currentuser = UserConstants.Users.FirstOrDefault(o => o.username.ToLower() == userlogin.username.ToLower() 
            && o.password == userlogin.password);

            if(currentuser != null)
            {
                return currentuser;
            }

            return null;
        }
    }
}