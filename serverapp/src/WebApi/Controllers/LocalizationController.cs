using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizationController : ControllerBase
    {
        [HttpPut("locale")]
        public IActionResult ChangeLanguageKey([FromBody] string locale)
        {
            Response.Cookies.Append("locale", locale, new CookieOptions()
            {
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok();
        }
    }
}
