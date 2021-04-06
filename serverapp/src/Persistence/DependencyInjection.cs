using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using Persistence.Context;
using Persistence.Interfaces;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseSqlServer(configuration.GetConnectionString("ApplicationDefaultDatabase")));
                //options.UseInMemoryDatabase("ApplicationDatabase"));
                options.UseNpgsql(configuration.GetConnectionString("ApplicationDatabase")));


            services.AddScoped<IUserProfileDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    }
}
