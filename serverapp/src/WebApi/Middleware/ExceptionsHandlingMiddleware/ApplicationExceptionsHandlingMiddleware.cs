using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Application.Exceptions;

using ApplicationException = Application.Exceptions.ApplicationException;

namespace WebApi.Middleware
{
    public class ApplicationExceptionsHandlingMiddleware:ExceptionsHandlingMiddleware
    {
        public ApplicationExceptionsHandlingMiddleware(RequestDelegate next, ILogger<ApplicationExceptionsHandlingMiddleware> logger) : base(next, logger) { }

       public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            switch (exception)
            {
                case NotFoundException ex:
                    LogException(ex);
                    await CreateErrorResponse(context, ex.Errors, HttpStatusCode.NotFound);
                    break;

                case ApplicationException ex:
                    LogException(ex);
                    await CreateErrorResponse(context, ex.Errors, HttpStatusCode.InternalServerError);
                    break;

                case Exception ex:
                    LogException(ex);
                    await CreateErrorResponse(context, "The request cannot be processed", HttpStatusCode.InternalServerError);
                    break;
            }
        }
    }
}
