using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Infrastructure.Identity.UseCases.Commands;
using Infrastructure.Identity.UseCases.Queries;
using Infrastructure.Identity.Interfaces;
using Application.UseCases.UserProfiles.Commands;
using Application.UseCases.UserProfiles.Queries;
using WebApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using MediatR;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagerService _userManagerService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserController(
            IUserManagerService userManagerService, 
            IMapper mapper, 
            IMediator mediator)
        {
            _userManagerService = userManagerService;
            _mapper = mapper;
            _mediator = mediator;
        }

        private string OriginUrl => Request.Scheme + "://" + Request.Host;

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] CreateAppUserCommand request)
        {

            //The user is created in two iterations: creating identity data and creating a user profile with public data
            var newAppUserId = await _mediator.Send(request);
            await _mediator.Send(new CreateUserProfileCommand { Id = newAppUserId });

            await _userManagerService.SendConfirmationEmail(request.Email, OriginUrl);

            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInQuery query)
        {
            var appUser = await _mediator.Send(query);
            var userModel = _mapper.Map<UserModel>(appUser);

            var userProfile = await _mediator.Send(new GetUserProfileQuery() { Id = appUser.Id });
            userModel = _mapper.Map(userProfile, userModel);

            return Ok(userModel);
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
            var confirmationResult = await _mediator.Send(new ConfirmEmailCommand() { Id = id, Token = token});

            var localUrl = $"/user/{id}/confirmemail" + (confirmationResult ? "success" : "failure");

            return LocalRedirect(localUrl);
        }

        [HttpGet("{email}/resetpasswordemail")]
        public async Task<IActionResult> SendResetPasswordEmail(string email)
        {
            await _userManagerService.SendResetPasswordEmail(email, OriginUrl);

            return Ok();
        }

        [HttpGet("{id}/verifytoken")]
        public async Task<IActionResult> VerifyRessetPasswordToken(string id, string token)
        {
            var verifyResult = await _userManagerService.VerifyResetPasswordTokenAsync(id, token);

            string localUrl;

            if (verifyResult)
            {
                Response.Cookies.Append("userId", id, new CookieOptions()
                {
                    Expires = DateTime.UtcNow.AddHours(4)
                });
                Response.Cookies.Append("resetToken", token, new CookieOptions()
                {
                    Expires = DateTime.UtcNow.AddHours(4)
                });
                localUrl = $"/user/{id}/resetpassword";
            }
            else
            {
                localUrl = $"/user/{id}/resetpasswordfailure";
            }

            return LocalRedirect(localUrl);
        }

        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            var userId = Request.Cookies["userId"];
            var resetToken = Request.Cookies["resetToken"];

            request.UserId = userId;
            request.Token = resetToken;

            var result = await _mediator.Send(request);

            var localUrl = $"/user/{userId}/resetpassword" + (result ? "success" : "failure");

            return LocalRedirect(localUrl);
        }
    }
}
