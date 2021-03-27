using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MediatR;

using Infrastructure;
using Infrastructure.Identity.DependencyInjection;
using Persistence;
using WebApi.Middleware;
using Application;

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

            services.AddCors();

            services.AddControllers();

          
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionsHandlingMiddleware>();

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
