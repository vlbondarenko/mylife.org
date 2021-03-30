using System.Threading.Tasks;
using System.Threading;

using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Persistence.Interfaces;

namespace Persistence.Context
{
    public class ApplicationDbContext:DbContext,IUserProfileDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
