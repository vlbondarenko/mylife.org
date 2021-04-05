using System;
using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using MediatR;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();

        protected string OriginUrl => Request.Scheme + "://" + Request.Host;

        protected IActionResult Created()
        {
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        protected void SetCookieValue(string key, string value)
        {
            var cookieoptions = new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddHours(4)
            };
            Response.Cookies.Append(key, value, cookieoptions);
        }


        protected string ExtractCookieValue(string key)
        {
            var value = Request.Cookies[key];

            var newCookieOptions = new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddDays(-1)
            };

            Response.Cookies.Delete(key, newCookieOptions);

            return value;
        }
    }
}
