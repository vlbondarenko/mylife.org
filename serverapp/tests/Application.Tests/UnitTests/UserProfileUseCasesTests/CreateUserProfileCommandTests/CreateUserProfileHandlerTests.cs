using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Xunit;

using Persistence.Context;
using Application.UseCases.UserProfiles.Commands;
using Application.Tests.Common;

namespace Application.Tests.UnitTests.UserProfileUseCasesTests.CreateProfileCommandTests
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
