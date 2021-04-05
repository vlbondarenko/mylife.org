using System.Threading.Tasks;
using System.Net;

using Xunit;

using WebApi.Tests.Common;
using Infrastructure.Identity.UseCases.Queries;
using WebApi.Models;

namespace WebApi.Tests.IntegrationTests.UserControllEndpointsTests
{
    public class SignInEndpointTests:UserControllerEndpointTestsBase
    {
        public SignInEndpointTests(ApiTestsFixture factory) : base(factory) { }

        [Fact]
        public async Task ReturnsSuccessStatusCode()
        {
            //Arrage
            var client = _factory.GetAnonymousClient();
            var query = new SignInQuery()
            {
                Email = "test@test.com",
                Password = "testpassword"
            };
            var content = Utilities.GetRequestContent(query);

            //Act
            var response = await client.PostAsync($"api/user/signin", content);

            //Assert
            response.EnsureSuccessStatusCode();

            var responseContent = await Utilities.GetResponseContent<UserModel>(response);

            Assert.NotNull(responseContent);
            Assert.NotNull(responseContent.Id);
            Assert.NotNull(responseContent.Email);
            Assert.NotNull(responseContent.UserName);
            Assert.NotNull(responseContent.AccessToken);
        }

        [Theory]
        [InlineData("invalid@test.com", "testpassword")]
        [InlineData("test@test.com", "invalidpassword")]
        public async Task ReturnsUnauthorizeStatusCode_WhenInvalidEmailOrPassword(string email, string password)
        {
            //Arrage
            var client = _factory.GetAnonymousClient();
            var query = new SignInQuery()
            {
                Email = email,
                Password = password
            };
            var content = Utilities.GetRequestContent(query);

            //Act
            var response = await client.PostAsync($"api/user/signin", content);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
