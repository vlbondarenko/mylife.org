using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using serverapp.Services;
using serverapp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace serverapp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;


        public AccountController(IAccountService accountService,UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

     
        [HttpPost("register")]
        public async Task<ActionResult<UserData>> SignUp([FromBody] SignUpData signUpData)
        {
            return await _accountService.SignUpAsync(signUpData);
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserData>> SignIn([FromBody] SignInData signInData)
        {
            return await _accountService.SignInAsync(signInData);
        }


        [Authorize]
        [HttpGet("getbyid")]
        public async Task<ActionResult<UserData>> GetById(string id)
        {
            return await _accountService.GetById(id);
        }


        [HttpGet("confirm-email")]
        public async Task ConfirmEmail (string id, string token)
        {
            //I don't know why, but in some strange way, from the token passed through the parameter in the original query string, the '+' character is replaced with a space, 
            //which prevents the successful confirmation of the email. Therefore, we first return the replaced characters to their place
            token = token.Replace(" ","+");
            await _accountService.ConfirmEmailAsync(id, token, HttpContext);
        }


        [HttpGet("send-confirm-email")]
        public async Task<dynamic> SendEmailConfirmationMessage(string id)
        {
            var originUrl = Request.Headers["origin"];

            return await _accountService.SendEmailConfirmationMessageAsync(id,originUrl);

        }

    }
}
