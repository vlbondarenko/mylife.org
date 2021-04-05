using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using Infrastructure.Identity.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class IdentityTokenClaimService:ITokenClaimsService
    {
        private SymmetricSecurityKey _key;

        public IdentityTokenClaimService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }

        public string CreateToken(string userId)
        {
            if (userId is null)
                throw new ArgumentNullException(nameof(userId));

            var claims = new List<Claim> { new Claim("userid", userId) };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
