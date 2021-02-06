using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using serverapp.Services;
using serverapp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;


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
            var newUser = await _accountService.SignUpAsync(signUpData);

            await SendEmailAdressConfirmationLinkAsync(newUser);

            return Ok();
        }


        [HttpPost("email-adress-confirmation")]
        public async Task<ActionResult> SendEmailAdressConfirmationMessage(string email)
        {
            var user = await _accountService.GetUserByEmailAsync(email);

            await SendEmailAdressConfirmationLinkAsync(user);

            return Ok();
        }


        [HttpGet("confirm-email")]
        public async Task ConfirmEmail(string id, string token)
        {
            //I don't know why, but in some strange way, from the token passed through the parameter in the original query string, the '+' character is replaced with a space, 
            //which prevents the successful confirmation of the email. Therefore, we first return the replaced characters to their place
            token = token.Replace(" ", "+");
            await _accountService.ConfirmEmailAdressAsync(id, token);

            Response.StatusCode = (int)HttpStatusCode.Moved;
            Response.Headers["location"] = "http://localhost:8080/confirm-email";
        }


        [HttpGet("reset-password")]
        public async Task ResetPassword(string email)
        {
            await _accountService.ResetPasswordAsync(email, HttpContext);
        }


       /* [Authorize]
        [HttpGet("getbyid")]
        public async Task<ActionResult<UserData>> GetById(string id)
        {
            return await _accountService.GetById(id);
        }  */

        private async Task SendEmailAdressConfirmationLinkAsync(AppUser user)
        {

            var confirmationToken = await _accountService.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/account/confirm-email?id={user.Id}&token={confirmationToken}";

            await _accountService.SendEmailAdressConfirmationMassageAsync(user.Email, confirmationLink);
        }
    }
}
