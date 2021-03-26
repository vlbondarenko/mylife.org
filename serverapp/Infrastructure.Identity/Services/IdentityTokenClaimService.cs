using System.Threading.Tasks;

using Infrastructure.Identity.Interfaces;

namespace Infrastructure.Identity.Services
{
    internal class IdentityTokenClaimService:ITokenClaimsService
    {
        public async Task<string> CreateToken(string userId)
        {
            return "";
        }
    }
}
