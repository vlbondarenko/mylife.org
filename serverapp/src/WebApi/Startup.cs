using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Application;
using Common;
using Infrastructure;
using Infrastructure.Identity.DependencyInjection;
using Persistence;
using WebApi.Middleware;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .AddJsonFile("appsettings.Infrastructure.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity(Configuration);

            services.AddInfrastructureServices(Configuration);

            services.AddPersistence(Configuration);

            services.AddApplication();

            services.AddCommonServices();

            services.AddCors();

            services.AddControllers();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionsHandlingMiddleware();

            app.UseCors();

            app.UseHttpsRedirection();
            
            app.UseRouting(); 

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
