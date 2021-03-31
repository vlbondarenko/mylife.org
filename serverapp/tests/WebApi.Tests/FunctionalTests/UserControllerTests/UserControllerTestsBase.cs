using Microsoft.Extensions.DependencyInjection;

using Xunit;

using WebApi.Tests.Common;
using Persistence.Context;
using Infrastructure.Identity.Data;

namespace WebApi.Tests.FunctionalTests.UserControllerTests
{
    public partial class UserControllerTestsBase:IClassFixture<ApiTestsFixture>
    {
        protected readonly ApiTestsFixture _factory;
        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly IdentityDbContext _identityDbContext;

        public UserControllerTestsBase(ApiTestsFixture factory)
        {
            _factory = factory;

            var scopeService = factory.Services.CreateScope();

            _applicationDbContext = scopeService.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _identityDbContext = scopeService.ServiceProvider.GetRequiredService<IdentityDbContext>();
        }  
    }
}
