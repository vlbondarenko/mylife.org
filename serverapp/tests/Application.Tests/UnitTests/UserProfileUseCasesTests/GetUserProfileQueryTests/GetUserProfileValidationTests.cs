using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using FluentValidation;
using FluentValidation.TestHelper;
using Application.UseCases.UserProfiles.Queries;

namespace Application.Tests.UnitTests.UserProfileUseCasesTests.GetUserProfileQueryTests
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
        public void ShoulBeErrorsWhenFieldsAreNullOrEmpty(string id)
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
