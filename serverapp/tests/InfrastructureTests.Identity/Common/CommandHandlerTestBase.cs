using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.Identity.Data;

namespace Infrastructure.Identity.Tests.Common
{
    public class CommandHandlerTestBase:IDisposable
    {
        protected readonly IdentityDbContext _identityContext;

        public CommandHandlerTestBase()
        {
            _identityContext = IdentityDbContextFactory.Create();
        }

        public void Dispose()
        {
            IdentityDbContextFactory.Destroy(_identityContext);
        }
    }
}
