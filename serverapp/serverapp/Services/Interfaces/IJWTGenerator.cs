using serverapp.Models;

namespace serverapp.Services
{
    public interface IJWTGenerator
    {
        string CreateToken(AppUser user);
    }
}
