using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

using Xunit;
using Microsoft.Extensions.Configuration;

using Infrastructure.Identity.Services;

namespace Infrastructure.Identity.Tests.UnitTests
{
    public class IdentityTokenClaimServiceTest
    {
        private readonly IConfiguration _configuration;
        private readonly IdentityTokenClaimService _identityTokenClaimService;

        public IdentityTokenClaimServiceTest()
        {
            var configurationBuilder = new ConfigurationBuilder().AddUserSecrets(typeof(Infrastructure.Identity.DependencyInjection.DependencyInjection).Assembly);
            _configuration = configurationBuilder.Build();

            _identityTokenClaimService = new IdentityTokenClaimService(_configuration);
        }


        [Fact]
        public void TokenSecretKeyFromUserSecretsIsNotNullAndValid()
        {
            var tokenKey = _configuration["TokenKey"];

            Assert.NotNull(tokenKey);
            //The secret key must be greater than 16 characters.
            //Otherwise an exception will be thrown when creating a jwt token
            Assert.True(tokenKey.Length > 16);
        }

        [Theory]
        [InlineData("04606c77-5c4a-4c77-8bcd-841b8f49431c")]
        public void CreatedTokenIsNotNullAndValid(string userId)
        {
            var tokenhandler = new JwtSecurityTokenHandler();

            var tokenAsString = _identityTokenClaimService.CreateToken(userId);
            var tokenAsObject = tokenhandler.ReadJwtToken(tokenAsString);
            var userIdClaim = tokenAsObject.Payload.Claims.First(claim => claim.Type == "userid");

            Assert.NotNull(tokenAsString);
            Assert.Equal(userId, userIdClaim.Value);
        }

        //The test is redundant.Just experimenting
        [Theory]
        [InlineData(null)]
        public void ThrowExceptionWhenUserIdIsNull(string userId)
        {
            Assert.Throws<ArgumentNullException>(() => _identityTokenClaimService.CreateToken(userId));
        }
    }
}
