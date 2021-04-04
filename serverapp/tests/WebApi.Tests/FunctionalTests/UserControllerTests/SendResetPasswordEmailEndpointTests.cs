using System.Threading.Tasks;
using System.Net;

using Xunit;

using WebApi.Tests.Common;

namespace WebApi.Tests.FunctionalTests.UserControllerTests
{
    public class SendResetPasswordEmailEndpointTests:UserControllerEndpointTestsBase
    {
        public SendResetPasswordEmailEndpointTests(ApiTestsFixture factory) : base(factory) { }

        [Fact]
        public async Task ReturnsSuccessStatusCode()
        {
            var client = _factory.GetAnonymousClient();
            var email = "test@test.com";

            var response = await client.GetAsync($"api/user/{email}/resetpasswordemail");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ReturnsNotFoundStatusCode_WhenUserNotFound()
        {
            var client = _factory.GetAnonymousClient();
            var email = "invalid@test.com";

            var response = await client.GetAsync($"api/user/{email}/resetpasswordemail");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
