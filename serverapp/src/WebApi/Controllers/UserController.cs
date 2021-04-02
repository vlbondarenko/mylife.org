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
    public class UserController : ApiControllerBase
    {

        private readonly IUserManagerService _userManagerService;

        public UserController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize([FromBody] CreateAppUserCommand request)
        {

            //The user is created in two iterations: creating identity data and creating a user profile with public data
            var newAppUserId = await Mediator.Send(request);
            await Mediator.Send(new CreateUserProfileCommand { Id = newAppUserId });

            await _userManagerService.SendConfirmationEmail(request.Email, OriginUrl);

            return Created();
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

        [HttpGet("{email}/confirmationemail")]
        public async Task<IActionResult> SendConfirmationEmail(string email)
        {
            await _userManagerService.SendConfirmationEmail(email, OriginUrl);

            return Ok();
        }

        [HttpGet("{id}/confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string id, string token)
        {
            var confirmationResult = await Mediator.Send(new ConfirmEmailCommand() { Id = id, Token = token});

            var localUrl = $"/user/{id}/confirmemail" + (confirmationResult ? "success" : "failure");

            return LocalRedirect(localUrl);
        }

        [HttpGet("send-reset-password-email")]
        public async Task<IActionResult> SendResetPasswordEmail(string userEmail)
        {
            await _userManagerService.SendResetPasswordEmail(userEmail, OriginUrl);
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
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            Request.Cookies.TryGetValue("userId", out string userId);
            Request.Cookies.TryGetValue("resetToken", out string token);

            request.UserId = userId;
            request.Token = token;

            await Mediator.Send(request);

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
