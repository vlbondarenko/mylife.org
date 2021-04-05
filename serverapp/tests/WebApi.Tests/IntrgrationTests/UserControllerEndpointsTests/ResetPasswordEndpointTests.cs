using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;

using Infrastructure.Identity.UseCases.Commands;

using Xunit;

using WebApi.Tests.Common;

namespace WebApi.Tests.IntegrationTests.UserControllEndpointsTests
{
    public class ResetPasswordEndpointTests : UserControllerEndpointTestsBase
    {
        public ResetPasswordEndpointTests(ApiTestsFixture factory) : base(factory) { }

        [Fact]
        public async Task PasswordResetSuccess_ReturnsSuccessStatusCode()
        {
            //Arrage
            var user = _identityDbContext.Users.First();
            var userId = user.Id;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = HttpUtility.UrlEncode(token);

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/user/resetpassword");
            request.Content = Utilities.GetRequestContent(new ResetPasswordCommand()
            {
                NewPassword = "newpassword"
            });
            request.Headers.Add("cookie", new string[] { $"userId={userId}", $"resetToken={token}" });

            var client = _factory.GetAnonymousClient();

            //Act
            var response = await client.SendAsync(request);

            //Assert
            var localUrl = $"/user/{userId}/resetpasswordsuccess";
            Assert.Equal(localUrl, response.RequestMessage.RequestUri.AbsolutePath);
        }
    }
}
