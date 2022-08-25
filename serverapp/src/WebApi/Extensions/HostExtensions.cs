using System.Threading.Tasks;
using Infrastructure.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Context;

namespace WebApi.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> MigrateDatabases(this IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            await serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>()!.Database.MigrateAsync();
            await serviceScope.ServiceProvider.GetRequiredService<IdentityDbContext>()!.Database.MigrateAsync();

            return host;
        }
    }
}
