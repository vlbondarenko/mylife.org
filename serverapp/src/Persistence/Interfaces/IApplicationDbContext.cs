using System.Threading.Tasks;
using System.Threading;

namespace Persistence.Interfaces
{
    public interface IApplicationDbContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
