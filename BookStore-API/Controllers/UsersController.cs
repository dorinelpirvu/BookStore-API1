﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore_API.Contracts;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


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
                return Ok(user);
            }

            return Unauthorized(userDTO);
        }
        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "ceva a mers rau");
        }
    }
}