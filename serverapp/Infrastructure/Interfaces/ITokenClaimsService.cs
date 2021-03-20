using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> CreateToken(string userId);
    }
}
