using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Xunit;

using Persistence.Interfaces;
using Application.UseCases.UserProfiles.Commands;
using Application.Tests.Common;

namespace Application.Tests.UnitTests
{
    public class CreateUserProfileHandlerTests
    {
        private readonly IUserProfileDbContext _userProfileDbContext;
        private readonly string _defaultUserId;

        public CreateUserProfileHandlerTests()
        {
            _defaultUserId = "00000000-0000-0000-0000-000000000000";
            _userProfileDbContext = DbContextFactory.CreateUserProfileDbContext();
        }

        [Fact]
        public void UserProfileGuaranteedCreatedInSingleInstance()
        {
            var command = new CreateUserProfileCommand() { Id = _defaultUserId };
            var handler = new CreateUserProfileCommand.CreateUserProfileCommandHandler(_userProfileDbContext);

            handler.Handle(command, CancellationToken.None).Wait();


            var userProfiles = _userProfileDbContext.UserProfiles.Where(up => up.Id == command.Id);
            Assert.True(userProfiles.Any());
            Assert.True(userProfiles.Count() == 1);
        }
    }
}
