using System.Linq;
using System.Threading;

using Xunit;

using Application.UseCases.UserProfiles.Commands;
using Application.Tests.Common;

namespace Application.Tests.UnitTests.UserProfileUseCasesTests
{
    public class CreateUserProfileHandlerTests:ApplicationHandlerTestsBase
    {
        [Fact]
        public void UserProfileGuaranteedCreatedInSingleInstance()
        {
            var command = new CreateUserProfileCommand() { Id = "11111111-1111-1111-1111-111111111111" };
            var handler = new CreateUserProfileCommand.CreateUserProfileCommandHandler(_applicationDbContext);

            handler.Handle(command, CancellationToken.None).Wait();


            var userProfiles = _applicationDbContext.UserProfiles.Where(up => up.Id == command.Id).ToArray();
            Assert.Single(userProfiles);
        }
    }
}
