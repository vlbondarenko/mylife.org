using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using FluentValidation;
using FluentValidation.TestHelper;

using Infrastructure.Identity.UseCases.Commands;

namespace Infrastructure.Identity.Tests.UnitTests.ConfirmEmailCommandTests
{
    public class ConfirmEmailCommandValidationTest
    {
        private readonly ConfirmEmailCommandValidator _validator;

        public ConfirmEmailCommandValidationTest()
        {
            _validator = new ConfirmEmailCommandValidator();
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void ShoulBeErrorsWhenFieldsAreNullOrEmpty(string id, string token)
        {
            var command = new ConfirmEmailCommand()
            {
                Id = id,
                Token = token
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(command => command.Id);
            result.ShouldHaveValidationErrorFor(command => command.Token);
        }
    }
}
