using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using FluentValidation;
using FluentValidation.TestHelper;

using Infrastructure.Identity.UseCases.Commands;

namespace Infrastructure.Identity.Tests.UnitTests.CreateAppUserCommandTests
{
    public class CreateAppUserCommandValidationTest
    {
        private readonly CreateAppUserCommandValidator _validator;

        public CreateAppUserCommandValidationTest()
        {
            _validator = new CreateAppUserCommandValidator();
        }

        [Theory]
        [InlineData(null,null,null)]
        [InlineData("","","")]
        public void ShoulBeErrorsWhenFieldsAreNullOrEmpty(string email, string userName, string password)
        {
            var command = new CreateAppUserCommand()
            {
                Email = email,
                UserName = userName,
                Password = password
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(command => command.Email);
            result.ShouldHaveValidationErrorFor(command => command.UserName);
            result.ShouldHaveValidationErrorFor(command => command.Password);
        }

        [Theory]
        [InlineData("test", true)]
        [InlineData("@test.com",true)]
        [InlineData("test@test.com", false)]
        public void ShouldBeErrorsWhenEmailNotValid(string email, bool shouldHaveErrors)
        {
            var command = new CreateAppUserCommand()
            {
                Email = email
            };

            var result = _validator.TestValidate(command);
            var haveErrors = result.Errors.Where(fail => fail.PropertyName == "Email").Any();

            Assert.Equal(shouldHaveErrors, haveErrors);
        }

        [Theory]
        [InlineData("test", true)]
        [InlineData("testpassword", false)]
        public void ShouldBeErrorsWhenPasswordNotValid(string password, bool shouldHaveErrors)
        {
            var command = new CreateAppUserCommand()
            {
                Password = password
            };

            var result = _validator.TestValidate(command);
            var haveErrors = result.Errors.Where(fail => fail.PropertyName == "Password").Any();

            Assert.Equal(shouldHaveErrors, haveErrors);
        }
    }
}
