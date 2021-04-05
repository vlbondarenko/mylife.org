using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices (this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
