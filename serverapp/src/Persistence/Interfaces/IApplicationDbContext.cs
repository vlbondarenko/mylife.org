using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
