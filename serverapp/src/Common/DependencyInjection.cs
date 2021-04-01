using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;

using Common.PipelineBehavior;

using Microsoft.Extensions.DependencyInjection;

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
