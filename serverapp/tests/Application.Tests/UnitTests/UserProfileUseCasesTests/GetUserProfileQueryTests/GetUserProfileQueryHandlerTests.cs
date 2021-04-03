using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Application.UseCases.UserProfiles.Queries;

using Application.Tests.Common;

namespace Application.Tests.UnitTests.UserProfileUseCasesTests.GetUserProfileQueryTests
{
    public class GetUserProfileQueryHandlerTests:ApplicationHandlerTestsBase
    {
        [Fact]
        public async Task ReturnsValueIsNotNull()
        {
            var query = new GetUserProfileQuery() { Id = "00000000-0000-0000-0000-000000000000" };
            var handler = new GetUserProfileQuery.GetUserProfileQueryHandler(_applicationDbContext,_mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.FirstName);
            Assert.NotNull(result.LastName);
            Assert.NotNull(result.BirthDate);
            Assert.NotNull(result.City);
            Assert.NotNull(result.Country);
        }
    }
}
