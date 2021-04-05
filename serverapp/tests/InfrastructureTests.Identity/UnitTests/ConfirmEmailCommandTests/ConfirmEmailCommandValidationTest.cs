using Xunit;

using FluentValidation.TestHelper;

using Infrastructure.Identity.UseCases.Commands;

namespace Infrastructure.Identity.Tests.UnitTests.UseCasesTests
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
        public void ShoulBeErrors_WhenFieldsAreNullOrEmpty(string id, string token)
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
