using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace WebApi.Middleware
{
    public abstract class ExceptionsHandlingMiddleware
    {
        protected readonly RequestDelegate _next;

        protected readonly ILogger<ExceptionsHandlingMiddleware> _logger;

        public ExceptionsHandlingMiddleware(RequestDelegate next, ILogger<ExceptionsHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public abstract Task InvokeAsync(HttpContext context);
        

        protected async Task CreateErrorResponse (HttpContext context, object errors, HttpStatusCode statusCode)
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

        protected void LogException(Exception e) =>
           _logger.LogError(e, "\n\tComponent: " + e.Source + "\n");
    }
}
