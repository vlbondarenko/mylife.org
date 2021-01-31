using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using serverapp.Models;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace serverapp.Infrastructure
{

    public interface IJWTGenerator
    {
        string CreateToken(AppUser user);
    }

    public class JWTGenerator:IJWTGenerator
    {
        private readonly SymmetricSecurityKey _key;

        public JWTGenerator ()
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret"));
        }

        public string CreateToken (AppUser user)
        {
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, user.UserName) };

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
