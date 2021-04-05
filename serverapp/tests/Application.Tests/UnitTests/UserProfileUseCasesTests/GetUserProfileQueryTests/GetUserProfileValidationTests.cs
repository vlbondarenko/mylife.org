using Xunit;
using FluentValidation.TestHelper;

using Application.UseCases.UserProfiles.Queries;

namespace Application.Tests.UnitTests.UserProfileUseCasesTests
{
    public class GetUserProfileQueryValidationTests
    {
        private readonly GetUserProfileQueryValidator _validator;

        public GetUserProfileQueryValidationTests()
        {
            _validator = new GetUserProfileQueryValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShoulBeErrors_WhenFieldsAreNullOrEmpty(string id)
        {
            var query = new GetUserProfileQuery()
            {
                Id = id
            };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(query => query.Id);
        }
    }
}
