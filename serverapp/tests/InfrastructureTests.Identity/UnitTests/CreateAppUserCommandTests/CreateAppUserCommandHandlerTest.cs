using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using System.Threading;

using Infrastructure.Identity.Tests.Common;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.UseCases.Commands;
using Infrastructure.Identity.Exceptions;

namespace Infrastructure.Identity.Tests.UnitTests.CreateAppUserCommandTests
{
    public class CreateAppUserCommandHandlerTest:CommandHandlerTestBase
    {

       [Theory]
       [InlineData("first","unique@unique.com" )]
       [InlineData("unique","first@mail.com")]
       public void ThrowExceptionWhenUserNameOrEmailAlreadyTaken(string userName, string email)
       {
            var userManager = MockHelper.MockUserManager<AppUser>().Object;
            var command = new CreateAppUserCommand() { UserName = userName, Email = email };
            var handler = new CreateAppUserCommand.CreateAppUserCommandHandler(userManager, _identityContext);

            Assert.ThrowsAsync<NotCreatedException>(() => handler.Handle(command, CancellationToken.None));
       }

        [Theory]
        [InlineData("unique", "unique@unique.com")]
        public void ReturnedUserIdValueNotNull(string userName, string email)
        {
            var mockMngr = MockHelper.MockUserManager<AppUser>();
            mockMngr.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            var command = new CreateAppUserCommand() { UserName = userName, Email = email, Password = "password" };
            var handler = new CreateAppUserCommand.CreateAppUserCommandHandler(mockMngr.Object, _identityContext);

            var task = handler.Handle(command, CancellationToken.None);

            Assert.NotNull(task.Result);
        }
    }
}
