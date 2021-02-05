using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using serverapp.Services;
using serverapp.Models;
using Microsoft.AspNetCore.Authorization;


namespace serverapp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("sign-in")]
        public async Task<UserData> SignIn([FromBody] SignInData signInData)
        {
            return await _accountService.SignInAsync(signInData);
        }


        [HttpPost("sign-up")]
        public async Task SignUp([FromBody] SignUpData signUpData)
        {
             await _accountService.SignUpAsync(signUpData, HttpContext);
        }


        [HttpGet("send-email-confirmation")]
        public async Task SendEmailConfirmationMessage(string email)
        {
            await _accountService.SendEmailAdressConfirmationMessageAsync(email, HttpContext.Request);
        }


        [HttpGet("confirm-email")]
        public async Task ConfirmEmail(string id, string token)
        {
            //I don't know why, but in some strange way, from the token passed through the parameter in the original query string, the '+' character is replaced with a space, 
            //which prevents the successful confirmation of the email. Therefore, we first return the replaced characters to their place
            token = token.Replace(" ", "+");
            await _accountService.ConfirmEmailAdressAsync(id, token, HttpContext.Response);
        }


        [HttpGet("reset-password")]
        public async Task ResetPassword(string email)
        {
            await _accountService.ResetPasswordAsync(email, HttpContext);
        }


        [Authorize]
        [HttpGet("getbyid")]
        public async Task<ActionResult<UserData>> GetById(string id)
        {
            return await _accountService.GetById(id);
        }  
    }
}
