using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Infrastructure.Identity.Exceptions;

namespace WebApi.Middleware
{
    public class IdentityExceptionsHandlingMiddleware:ExceptionsHandlingMiddleware
    {
        public IdentityExceptionsHandlingMiddleware(RequestDelegate next, ILogger<IdentityExceptionsHandlingMiddleware> logger):base(next, logger) { }

        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            switch (exception)
            {
                case NotCreatedException ex:
                    LogException(exception);
                    await CreateErrorResponse(context, ex.Errors, HttpStatusCode.BadRequest);
                    break;
                case NotFoundException ex:
                    LogException(ex);
                    await CreateErrorResponse(context, ex.Errors, HttpStatusCode.NotFound);
                    break;

                case UnauthorizedException ex:
                    LogException(ex);
                    await CreateErrorResponse(context, ex.Errors, HttpStatusCode.Unauthorized);
                    break;

                case IdentityException ex:
                    LogException(ex);
                    await CreateErrorResponse(context, ex.Errors, HttpStatusCode.InternalServerError);
                    break;

                case Exception e:
                    LogException(e);
                    await CreateErrorResponse(context, "The request cannot be processed", HttpStatusCode.InternalServerError);
                    break;
            }
        }
    }
}
