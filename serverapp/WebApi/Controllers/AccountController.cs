using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

using MediatR;
using Infrastructure.Identity.Commands;
using Infrastructure.Identity.Queries;
using Infrastructure.Identity.Interfaces;
using Application.UseCases.UserProfiles.Commands;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {

        private readonly IUserManagerService _userManagerService;

        public AccountController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }


        [HttpPost("login")]
        public async Task<UserInfo> Login([FromBody] LoginQuery query)
        {
            var appUser = await Mediator.Send(query);

            return new UserInfo()
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                CreatedAt = appUser.CreatedAt,
                AccessToken = appUser.AccessToken
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody] CreateAppUserCommand createAppUserCommand)
        {

            //The user is created in two iterations: creating identity data and creating a user profile with public data
            var newAppUserId = await Mediator.Send(createAppUserCommand);

            await Mediator.Send(new CreateUserProfileCommand { Id = newAppUserId });

            await _userManagerService.SendConfirmationEmail(createAppUserCommand.Email);

            return Ok();
        }

        [HttpGet("send-confirmation-email")]
        public async Task<IActionResult> SendConfirmEmail(string userEmail)
        {
            await _userManagerService.SendConfirmationEmail(userEmail);
            return Ok();
        }

        [HttpGet("confirm-email")]
        public async Task ConfirmEmail([FromQuery] ConfirmEmailCommand confirmEmailCommand)
        {
            try
            {
                //I don't know why, but in some strange way, from the token passed through the parameter in the original query string, the '+' character is replaced with a space, 
                //which prevents the successful confirmation of the email. Therefore, we first return the replaced characters to their place
                confirmEmailCommand.Token = confirmEmailCommand.Token.Replace(" ", "+");
                await Mediator.Send(confirmEmailCommand);

                RedirectClientToLocation(OriginUrl + "/confirm-email-success");
            }
            catch
            {
                RedirectClientToLocation(OriginUrl + "/confirm-email-failure");
            }
        }

        [HttpGet("send-reset-password-email")]
        public async Task<IActionResult> SendResetPasswordEmail(string userEmail)
        {
            await _userManagerService.SendResetPasswordEmail(userEmail);
            return Ok();
        }

        [HttpGet("verify-token")]
        public async Task VerifyRessetPasswordToken(string id, string token)
        {
            try
            {
                token = token.Replace(" ", "+");
                var result = await _userManagerService.VerifyResetPasswordTokenAsync(id, token);
                if (result)
                {
                    SetCookie("userId", id);
                    SetCookie("resetToken", token);
                    RedirectClientToLocation(OriginUrl + "/reset-password");
                }
                else
                    throw new Exception();
            }
            catch
            {
                RedirectClientToLocation(OriginUrl + "/reset-password-failure");
            }
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            Request.Cookies.TryGetValue("userId", out string userId);
            Request.Cookies.TryGetValue("resetToken", out string token);
            await _userManagerService.ResetPasswordAsync(userId, token, resetPasswordModel.NewPassword);

            return Ok();
        }

        private void RedirectClientToLocation(string location)
        {
            Response.StatusCode = (int)HttpStatusCode.Moved;
            Response.Headers["location"] = location;
        }


        private void SetCookie(string key, string value)
        {
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append(key, value, cookiesOptions);
        }
    }
}
