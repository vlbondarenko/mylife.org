using System.Threading.Tasks;
using System.Net;

using Xunit;

using WebApi.Tests.Common;

namespace WebApi.Tests.FunctionalTests.UserControllerTests
{
    public class SendConfirmationEmailEndpointTests:UserControllerEndpointTestsBase
    {
        public SendConfirmationEmailEndpointTests(ApiTestsFixture factory) : base(factory) { }

        [Fact]
        public async Task ReturnsSuccessStatusCode()
        {
            var client = _factory.GetAnonymousClient();
            var email = "test@test.com";

            var response = await client.GetAsync($"api/user/{email}/confirmationemail");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ReturnsNotFoundStatusCode_WhenUserNotFound()
        {
            var client = _factory.GetAnonymousClient();
            var email = "invalid@test.com";

            var response = await client.GetAsync($"api/user/{email}/confirmationemail");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
