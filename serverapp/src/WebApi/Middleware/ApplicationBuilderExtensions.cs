using Microsoft.AspNetCore.Builder;

namespace WebApi.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionsHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionsHandlingMiddleware>();

            return app;
        }
    }
}
