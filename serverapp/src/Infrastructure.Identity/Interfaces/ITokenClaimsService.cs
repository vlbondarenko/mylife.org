using System.Threading.Tasks;

namespace Infrastructure.Identity.Interfaces
{
    public interface ITokenClaimsService
    {
        string CreateToken(string userId);
    }
}
