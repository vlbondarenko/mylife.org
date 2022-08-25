using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace WebApi.Common
{
    public class CustomCookieRequestCultureProvider : RequestCultureProvider
    {
        public string CookieName { get; set; } = "locale";

        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var cookie = httpContext.Request.Cookies[CookieName];

            if (string.IsNullOrEmpty(cookie))
            {
                return NullProviderCultureResult;
            }

            return Task.FromResult(new ProviderCultureResult(cookie, cookie));
        }

        public static string MakeCookieValue(RequestCulture requestCulture)
        {
            if (requestCulture == null)
            {
                throw new ArgumentNullException(nameof(requestCulture));
            }

            return requestCulture.Culture.Name;
        }
    }
}
