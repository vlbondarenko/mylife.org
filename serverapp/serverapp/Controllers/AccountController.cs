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
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService,UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
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

        [HttpGet("confirm-email")]
        public async Task ConfirmEmail (string id, string token)
        {
            var result = _userService.ConfirmEmail(id, token);
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

            return await _userService.SendConfirmEmailMessage(user,Request.Headers["origin"]);

        }

    }
}
