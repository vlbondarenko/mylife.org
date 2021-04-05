using Microsoft.Extensions.DependencyInjection;

using MediatR;
using FluentValidation;

using Common.PipelineBehavior;

namespace Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            //Disabling localization of validator error messages. With this setup, the error messages will be in English
            ValidatorOptions.Global.LanguageManager.Enabled = false;

            return services;
        }
    }
}
