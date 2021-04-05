using System;

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
