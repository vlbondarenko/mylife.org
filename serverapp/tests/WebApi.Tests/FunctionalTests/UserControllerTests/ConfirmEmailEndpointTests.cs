using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;

using Xunit;

using WebApi.Tests.Common;

namespace WebApi.Tests.FunctionalTests.UserControllerTests
{
    public class ConfirmEmailEndpointTests : UserControllerEndpointTestsBase
    {
        public ConfirmEmailEndpointTests(ApiTestsFixture factory) : base(factory) { }

        [Fact]
        public async Task EmailConfirmationSuccess_RedirectClientToSuccessPage()
        {
            var client = _factory.GetAnonymousClient();
            var user = _identityDbContext.Users.First();
            var userId = user.Id;
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = HttpUtility.UrlEncode(token);

            var response = await client.GetAsync($"api/user/{userId}/confirmemail?token={token}");

            var localUrl = $"/user/{userId}/confirmemailsuccess";
            Assert.Equal(localUrl,response.RequestMessage.RequestUri.AbsolutePath);
        }


        [Fact]
        public async Task EmailConfirmationFailure_RedirectClientToFailurePage()
        {
            var client = _factory.GetAnonymousClient();
            var user = _identityDbContext.Users.First();
            var userId = user.Id;
            var invalidToken = "invalidtoken";

            var response = await client.GetAsync($"api/user/{userId}/confirmemail?token={invalidToken}");

            var localUrl = $"/user/{userId}/confirmemailfailure";
            Assert.Equal(localUrl, response.RequestMessage.RequestUri.AbsolutePath);
        }

        [Fact]
        public async Task ReturnsNotFoundStatusCodeWhenUserNotExis()
        {
            var client = _factory.GetAnonymousClient();
            var userId = "invaliduserid";
            var token = Guid.NewGuid().ToString();

            var response = await client.GetAsync($"api/user/{userId}/confirmemail?token={token}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
