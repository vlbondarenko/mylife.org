using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using serverapp.Services;
using serverapp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using serverapp.Infrastructure;


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
        public async Task<ActionResult> SignUp([FromBody] SignUpData signUpData)
        {
            await _accountService.SignUpAsync(signUpData);
            await _accountService.SendEmailAdressConfirmationMassageAsync(signUpData.Email);

            return Ok();
        }


        //Action to resend an email with a link to confirm the email address
        [HttpPost("email-adress-confirmation")]
        public async Task<ActionResult> SendEmailAdressConfirmationMessage(string email)
        {
            await _accountService.SendEmailAdressConfirmationMassageAsync(email); 

            return Ok();
        }


        [HttpGet("confirm-email")]
        public async Task ConfirmEmail(string id, string token)
        {
            try
            {
                //I don't know why, but in some strange way, from the token passed through the parameter in the original query string, the '+' character is replaced with a space, 
                //which prevents the successful confirmation of the email. Therefore, we first return the replaced characters to their place
                token = token.Replace(" ", "+");
                await _accountService.ConfirmEmailAdressAsync(id, token);

                RedirectClientToLocation("http://localhost:8080/confirm-email-success");
            }
            catch
            {
                RedirectClientToLocation("http://localhost:8080/confirm-email-failure");
            }
        }


        [HttpGet("forgot-password")]
        public async Task ForgotPassword(string email)
        {
            await _accountService.ResetPasswordAsync(email, HttpContext);
        }


        private void RedirectClientToLocation (string location)
        {
            Response.StatusCode = (int)HttpStatusCode.Moved;
            Response.Headers["location"] = location;
        }
    }
}
