using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FluentValidation;

using Newtonsoft.Json;

using Infrastructure.Identity.Exceptions;

using ValidationException = Common.Exceptions.ValidationException;

namespace WebApi.Middleware
{
    public class ExceptionsHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionsHandlingMiddleware> _logger;

        public ExceptionsHandlingMiddleware(RequestDelegate next, ILogger<ExceptionsHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

                case ValidationException validationException:
                    await CreateErrorResponse(validationException.Errors, context, HttpStatusCode.UnprocessableEntity);
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
                case UserNotCreatedException notCreatedException:
                    LogException(notCreatedException, "User creation failure");
                    await CreateErrorResponse(notCreatedException.Errors, context, HttpStatusCode.BadRequest);
                    break;
                case UserNotFoundException notFoundException:
                    LogException(notFoundException, "User not found");
                    await CreateErrorResponse(notFoundException.Errors, context, HttpStatusCode.NotFound);
                    break;

                case UnauthorizedException unauthorizedException:
                    LogException(unauthorizedException, "User not found");
                    await CreateErrorResponse(unauthorizedException.Errors, context, HttpStatusCode.Unauthorized);
                    break;

                case IdentityException identityException:
                    LogException(identityException, "Identity error");
                    await CreateErrorResponse(identityException.Errors, context, HttpStatusCode.InternalServerError);
                    break;
            }
        }

        private void LogException(Exception e, string message)=>
            _logger.LogError(e,message);
        

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
