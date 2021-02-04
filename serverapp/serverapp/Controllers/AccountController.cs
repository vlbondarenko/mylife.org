using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using serverapp.Services;
using serverapp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // POST api/<AccountController>
        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register([FromBody] RegisterModel registerData)
        {
            return await _accountService.Register(registerData);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody] LoginModel loginData)
        {
            return await _accountService.Login(loginData);
        }


        [Authorize]
        [HttpGet("getbyid")]
        public async Task<ActionResult<UserModel>> GetById(string id)
        {
            return await _accountService.GetById(id);
        }

        [HttpGet("confirm-email")]
        public async Task ConfirmEmail (string id, string token)
        {

            //I don't know why, but in some strange way, from the token passed through the parameter in the original query string, the '+' character is replaced with a space, 
            //which prevents the successful confirmation of the email. Therefore, we first return the replaced characters to their place
            token = token.Replace(" ","+");
            var result = _accountService.ConfirmEmail(id, token);
            result.Wait();
            if (result.IsCompleted)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.OK;
                await HttpContext.Response.WriteAsync("Your email confirmed succesfully!");
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HttpContext.Response.WriteAsync("Your email not confirmed");
            }
        }

        [HttpGet("send-confirm-email")]
        public async Task<dynamic> SendConfirmEmailMessage(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return await _accountService.SendConfirmEmailMessage(user,Request.Headers["origin"]);

        }

    }
}
