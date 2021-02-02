using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using serverapp.Infrastructure;
using serverapp.Services;
using serverapp.Models;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace serverapp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/<AccountController>
        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register([FromBody] RegisterModel registerData)
        {
            return await _userService.Register(registerData);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody] LoginModel loginData)
        {
            return await _userService.Login(loginData);
        }


        [Authorize]
        [HttpGet("getbyid")]
        public async Task<ActionResult<UserModel>> GetById(string id)
        {
            return await _userService.GetById(id);
        }
    }
}
