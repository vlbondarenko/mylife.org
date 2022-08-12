using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizationController : ApiControllerBase
    {
        [HttpPut("locale")]
        public async Task<IActionResult> ChangeLanguageKey([FromBody] string locale)
        {
            var cookieValue = $"c={locale}|uic={locale}";

            SetCookieValue(".AspNetCore.Culture", cookieValue, new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddHours(4),
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok();
        }
    }
}
