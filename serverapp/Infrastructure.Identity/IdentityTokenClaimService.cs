using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.Interfaces;

namespace Infrastructure.Identity
{
    class IdentityTokenClaimService:ITokenClaimsService
    {
        public async Task<string> CreateToken(string userId)
        {
            return "";
        }
    }
}
