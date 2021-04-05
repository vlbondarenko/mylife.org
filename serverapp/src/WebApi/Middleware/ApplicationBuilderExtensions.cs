using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace WebApi.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionsHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApplicationExceptionsHandlingMiddleware>();
            app.UseMiddleware<IdentityExceptionsHandlingMiddleware>();

            return app;
        }
    }
}
