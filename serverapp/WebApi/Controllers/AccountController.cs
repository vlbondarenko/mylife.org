using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using Infrastructure.Identity.Commands;
using Infrastructure.Identity.Interfaces;
using Application.UseCases.UserProfiles.Commands;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {

        private readonly IUserManagerService _userManager;

        public AccountController(IUserManagerService userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register ([FromBody] CreateAppUserCommand createAppUserCommand)
        {

            //The user is created in two iterations: creating identity data and creating a user profile with public data
            var newAppUserId = await Mediator.Send(createAppUserCommand);

            await Mediator.Send(new CreateUserProfileCommand { Id = newAppUserId });

            await _userManager.SendConfirmationEmail(createAppUserCommand.Email);

            return Ok();
        }
    }
}
