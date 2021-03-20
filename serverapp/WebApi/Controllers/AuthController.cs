using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using Infrastructure.Identity.Commands;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration ([FromBody] CreateUserCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
