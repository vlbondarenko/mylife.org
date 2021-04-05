using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IApplicationDbContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
