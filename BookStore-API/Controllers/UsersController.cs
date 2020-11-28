using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore_API.Contracts;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ILoggerService _logger;

        public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IConfiguration config,
            ILoggerService logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _logger = logger;
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserDTO userDTO)
        {
            try
            {
               var name = userDTO.Name;
               var password = userDTO.Password;
                var user = new IdentityUser { Email = name, UserName = name };
                var result = await _userManager.CreateAsync(user,password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        return InternalError($"{error.Description}");
                    }
                    
                }
            return Ok(new { result.Succeeded });
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
               
            }
            
        }
        
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async  Task<IActionResult> Login([FromBody]UserDTO userDTO)
        {
            var name = userDTO.Name;
            var password = userDTO.Password;
            var result = await _signInManager.PasswordSignInAsync(name, password, false, false);


            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(name);

                var tokenString = await GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }

            return Unauthorized(userDTO);
        }


        private async Task<string> GenerateJSONWebToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                , _config["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

            private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "ceva a mers rau");
        }
    }
}
