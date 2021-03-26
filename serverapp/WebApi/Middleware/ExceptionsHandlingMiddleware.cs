using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using Infrastructure.Identity.Exceptions;

namespace WebApi.Middleware
{
    public class ExceptionsHandlingMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionsHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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
                case IdentityException identityException:
                    await HandleIdentityException(identityException, context);
                    break;

                case Exception e:
                    var errors = string.IsNullOrWhiteSpace(e.Message) ? "The request cannot be processed" : e.Message;
                    await CreateErrorResponse(errors, context, HttpStatusCode.InternalServerError);
                    break;
            }
        }

        private async Task HandleIdentityException (IdentityException exception, HttpContext context)
        {
            switch (exception)
            {
                case UserNotCreatedException createdException:
                    await CreateErrorResponse(createdException.Errors, context, HttpStatusCode.BadRequest);
                    break;
                case IdentityException identityException:
                    await CreateErrorResponse(identityException.Errors, context, HttpStatusCode.InternalServerError);
                    break;
            }
        }

        private async Task CreateErrorResponse (object errors, HttpContext context, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "appliation/json";

            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
