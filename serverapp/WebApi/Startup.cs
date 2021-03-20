using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

using MediatR;

using Infrastructure;
using Persistence;
using WebApi.Middleware;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

<<<<<<< HEAD
=======
            services.AddMediatR(Assembly.GetExecutingAssembly());

>>>>>>> a77b91fdbc6c3a05d478c5f2d2a82c71b79b2e84
            services.AddIdentity(Configuration);

            services.AddInfrastructureServices();

            services.AddPersistence(Configuration);

            services.AddCors();

            services.AddControllers();
<<<<<<< HEAD

            services.AddMediatR(typeof(Infrastructure.DependencyInjection).Assembly);
=======
>>>>>>> a77b91fdbc6c3a05d478c5f2d2a82c71b79b2e84
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
