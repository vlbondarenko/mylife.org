using System;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public interface ITokenClaimsService
    {
        Task<string> CreateToken(string userId);
    }
}
