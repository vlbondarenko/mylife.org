using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence.Interfaces
{
    public interface IUserProfileDbContext:IApplicationDbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
