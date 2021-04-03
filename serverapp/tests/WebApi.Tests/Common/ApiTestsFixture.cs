using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Infrastructure.Identity.Data;
using Persistence.Context;
using Persistence.Interfaces;

namespace WebApi.Tests.Common
{
    public class ApiTestsFixture:WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost (IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();
                
                //you need to remove the descriptors for the database contexts,
                //otherwise the database configured in the Startup class will be used,
                //and the UseInMemoryDatabase() option will not work
                var appDbDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (appDbDescriptor != null)
                    services.Remove(appDbDescriptor);

                var identityDbDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<IdentityDbContext>));
                if (identityDbDescriptor != null)
                    services.Remove(identityDbDescriptor);

                services.AddDbContext<IdentityDbContext>(options =>
                {
                    options.UseInMemoryDatabase("IdentityTestDatabase");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("ApplicationTestDatabase");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddScoped<IUserProfileDbContext>(provider => provider.GetService<ApplicationDbContext>());

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var identityContext = scopedServices.GetRequiredService<IdentityDbContext>();
                var appContext = scopedServices.GetRequiredService<ApplicationDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<ApiTestsFixture>>();

                identityContext.Database.EnsureCreated();
                appContext.Database.EnsureCreated();


                try
                {
                    Utilities.InitializeDbForTest(identityContext, appContext);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                                        $"database with test messages. Error: {ex.Message}");
                }

            });
            
        }

        public T GetService<T>()
        {
            var scopedServices = Services.CreateScope();
            var service = scopedServices.ServiceProvider.GetRequiredService<T>();
            return service;
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
