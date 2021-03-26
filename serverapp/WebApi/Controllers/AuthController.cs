using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using Infrastructure.Identity.Commands;
using Infrastructure.Identity.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {

        private readonly IUserManagerService _userManager;

        public AuthController(IUserManagerService userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register ([FromBody] CreateAppUserCommand request)
        {
            await Mediator.Send(request);
            await _userManager.SendConfirmationEmail(request.Email);
            return Ok();
        }
    }
}
