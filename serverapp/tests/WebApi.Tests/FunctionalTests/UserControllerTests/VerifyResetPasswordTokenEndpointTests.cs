using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Collections.Generic;

using Xunit;

using WebApi.Tests.Common;

namespace WebApi.Tests.FunctionalTests.UserControllerTests
{
    public class VerifyResetPasswordTokenEndpointTests : UserControllerEndpointTestsBase
    {
        public VerifyResetPasswordTokenEndpointTests(ApiTestsFixture factory) : base(factory) { }

        [Fact]
        public async Task VerifySuccess_RedirectClientToResetPasswordPage()
        {
            var client = _factory.GetAnonymousClient();
            var user = _identityDbContext.Users.First();
            var userId = user.Id;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = HttpUtility.UrlEncode(token);

            var response = await client.GetAsync($"api/user/{userId}/verifytoken?token={token}");

            var localUrl = $"/user/{userId}/resetpassword";
            Assert.Equal(localUrl,response.RequestMessage.RequestUri.AbsolutePath);

            response.RequestMessage.Headers.TryGetValues("cookie", out IEnumerable<string> cookieValues);
            Assert.True(cookieValues.First(value => value.Contains("userId")).Any());
            Assert.True(cookieValues.First(value => value.Contains("resetToken")).Any());
        }


        [Fact]
        public async Task VerifyFailure_RedirectClientToFailurePage()
        {
            var client = _factory.GetAnonymousClient();
            var user = _identityDbContext.Users.First();
            var userId = user.Id;
            var invalidToken = "invalidtoken";

            var response = await client.GetAsync($"api/user/{userId}/verifytoken?token={invalidToken}");

            var localUrl = $"/user/{userId}/resetpasswordfailure";
            Assert.Equal(localUrl, response.RequestMessage.RequestUri.AbsolutePath);
        }

        [Fact]
        public async Task ReturnsNotFoundStatusCodeWhenUserNotExis()
        {
            var client = _factory.GetAnonymousClient();
            var userId = "invaliduserid";
            var token = Guid.NewGuid().ToString();

            var response = await client.GetAsync($"api/user/{userId}/verifytoken?token={token}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
