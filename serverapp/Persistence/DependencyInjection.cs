using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using Persistence.Context;
<<<<<<< HEAD
using Persistence.Interfaces;
=======
>>>>>>> a77b91fdbc6c3a05d478c5f2d2a82c71b79b2e84

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
<<<<<<< HEAD
                options.UseNpgsql(configuration.GetConnectionString("ApplicationDatabase")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

=======
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
>>>>>>> a77b91fdbc6c3a05d478c5f2d2a82c71b79b2e84
            return services;
        }
    }
}
