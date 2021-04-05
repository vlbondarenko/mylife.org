using Microsoft.EntityFrameworkCore;

using Infrastructure.Identity.Data;

namespace Infrastructure.Identity.Tests.Common
{
    public class IdentityDbContextFactory
    {
        public static IdentityDbContext Create()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase("TestIdentityDb")
                .Options;

            var context = new IdentityDbContext(options);

            context.Database.EnsureCreated();

            context.Users.AddRange(new[] {
                new AppUser() { UserName = "first", Email = "first@mail.com" },
                new AppUser() { UserName = "second", Email = "second@mail.com" }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(IdentityDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
