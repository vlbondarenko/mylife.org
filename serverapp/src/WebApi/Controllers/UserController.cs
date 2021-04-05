using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Infrastructure.Identity.UseCases.Commands;
using Infrastructure.Identity.UseCases.Queries;
using Infrastructure.Identity.Interfaces;
using Application.UseCases.UserProfiles.Commands;
using Application.UseCases.UserProfiles.Queries;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {

        private readonly IUserManagerService _userManagerService;
        private readonly IMapper _mapper;

        public UserController(IUserManagerService userManagerService, IMapper mapper)
        {
            _userManagerService = userManagerService;
            _mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] CreateAppUserCommand request)
        {

            //The user is created in two iterations: creating identity data and creating a user profile with public data
            var newAppUserId = await Mediator.Send(request);
            await Mediator.Send(new CreateUserProfileCommand { Id = newAppUserId });

            await _userManagerService.SendConfirmationEmail(request.Email, OriginUrl);

            return Created();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInQuery query)
        {
            var appUser = await Mediator.Send(query);
            var userModel = _mapper.Map<UserModel>(appUser);

            var userProfile = await Mediator.Send(new GetUserProfileQuery() { Id = appUser.Id });
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
            var confirmationResult = await Mediator.Send(new ConfirmEmailCommand() { Id = id, Token = token});

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
                SetCookieValue("userId", id);
                SetCookieValue("resetToken", token);
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
            var userId = ExtractCookieValue("userId");
            var token = ExtractCookieValue("resetToken");

            request.UserId = userId;
            request.Token = token;

            var result = await Mediator.Send(request);

            var localUrl = $"/user/{userId}/resetpassword" + (result ? "success" : "failure");

            return LocalRedirect(localUrl);
        }
    }
}
