using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

using Xunit;

using WebApi.Tests.Common;
using Persistence.Context;
using Infrastructure.Identity.Data;

namespace WebApi.Tests.FunctionalTests.UserControllerTests
{
    public partial class UserControllerEndpointTestsBase:IClassFixture<ApiTestsFixture> 
    {
        protected readonly ApiTestsFixture _factory;
        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly IdentityDbContext _identityDbContext;
        protected readonly UserManager<AppUser> _userManager;

        public UserControllerEndpointTestsBase(ApiTestsFixture factory)
        {
            _factory = factory;
            _applicationDbContext = factory.GetService<ApplicationDbContext>();
            _identityDbContext = factory.GetService<IdentityDbContext>();
            _userManager = factory.GetService<UserManager<AppUser>>();
        }  
    }
}
