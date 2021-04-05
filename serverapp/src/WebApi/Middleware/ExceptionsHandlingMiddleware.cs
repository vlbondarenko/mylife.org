using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Infrastructure.Identity.Exceptions;

using ApplicationException = Application.Exceptions.ApplicationException;
using ValidationException = Common.Exceptions.ValidationException;

namespace WebApi.Middleware
{
    public class ExceptionsHandlingMiddleware
    {
        protected readonly RequestDelegate _next;

        protected readonly ILogger<ExceptionsHandlingMiddleware> _logger;

        public ExceptionsHandlingMiddleware(RequestDelegate next, ILogger<ExceptionsHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public virtual async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case IdentityException ex:
                    await HandleIdentityExceptionsAsync(context, ex);
                    break;

                case ApplicationException ex:
                    await HandleApplicationExceptionsAsync(context, ex);
                    break;

                case ValidationException ex:
                    LogException(ex);
                    await CreateErrorResponse(context, ex.Errors, HttpStatusCode.UnprocessableEntity);
                    break;

                case Exception ex:
                    LogException(ex);
                    await CreateErrorResponse(context, "The request cannot be processed", HttpStatusCode.InternalServerError);
                    break;
            }
        }

        private async Task HandleIdentityExceptionsAsync(HttpContext context, Exception exception)
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
            }
        }

        private async Task HandleApplicationExceptionsAsync(HttpContext context, Exception exception)
        {

            switch (exception)
            {
                case Application.Exceptions.NotFoundException ex:
                    LogException(ex);
                    await CreateErrorResponse(context, ex.Errors, HttpStatusCode.NotFound);
                    break;

                case ApplicationException ex:
                    LogException(ex);
                    await CreateErrorResponse(context, ex.Errors, HttpStatusCode.InternalServerError);
                    break;
            }
        }

        protected void LogException(Exception e) =>
            _logger.LogError(e, "\n\tComponent: " + e.Source + "\n");

        protected async Task CreateErrorResponse(HttpContext context, object errors, HttpStatusCode statusCode)
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
