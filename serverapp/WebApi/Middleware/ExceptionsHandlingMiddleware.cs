using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Infrastructure.Identity.Exceptions;

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
            object error = null;

            switch (exception)
            {
                case IdentityException identity:
                    _logger.LogError(exception, "Rest error");
                    error = identity.ErrorMessage;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case Exception e:
                    _logger.LogError(exception, "Server error");
                    error = string.IsNullOrWhiteSpace(e.Message) ? "The request cannot be processed" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "appliation/json";

            if (error != null)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    error
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
