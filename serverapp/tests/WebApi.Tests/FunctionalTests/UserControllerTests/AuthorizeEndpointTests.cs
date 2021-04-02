using System.Linq;
using System.Threading.Tasks;
using System.Net;

using Xunit;

using WebApi.Tests.Common;
using Infrastructure.Identity.Commands;

namespace WebApi.Tests.FunctionalTests.UserControllerTests
{
    public class AuthorizeEndpointTest:UserControllerEndpointTestsBase
    {
        public AuthorizeEndpointTest(ApiTestsFixture factory) : base(factory) { }

        [Fact]
        public async Task ReturnsSuccessStatusCode()
        {
            //Arrage
            var client = _factory.GetAnonymousClient();
            var command = new CreateAppUserCommand()
            {
                Email = "email@email.com",
                UserName = "email",
                Password = "testpassword"
            };
            var content = Utilities.GetRequestContent(command);

            //Act
            var response = await client.PostAsync($"api/user/authorize", content);

            //Assert
            response.EnsureSuccessStatusCode();

            var appUsers = _identityDbContext.Users.Where(user => user.Email == command.Email && user.UserName == command.UserName).ToArray();
            Assert.Single(appUsers);

            var profiles = _applicationDbContext.UserProfiles.Where(userProfile => userProfile.Id == appUsers[0].Id).ToArray();
            Assert.Single(profiles);
        }

        [Theory]
        [InlineData("test@test.com","unique","somepassword")]
        [InlineData("unique@unique.com", "test", "somepassword")]
        public async Task ReturnsBadRequestStatusCode_WhenEmailOrUsernameAlreadyTaken(string email, string userName, string password)
        {
            var client = _factory.GetAnonymousClient();
            var command = new CreateAppUserCommand()
            {
                Email = email,
                UserName = userName,
                Password = password
            };
            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"api/user/authorize", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(null, null, null)]
        [InlineData("unique","unique","somepassword")]
        [InlineData("unique@unique.com","unique","pswd")]
        public async Task ReturnsUnprocessableEntityStatusCode_WhenDataIsNotValid(string email, string userName, string password)
        {
            var client = _factory.GetAnonymousClient();
            var command = new CreateAppUserCommand()
            {
                Email = email,
                UserName = userName,
                Password = password
            };
            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"api/user/authorize", content);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

    }
}
