using serverapp.Models;

namespace serverapp.Services.Interfaces
{
    public interface IJWTGenerator
    {
        string CreateToken(AppUser user);
    }
}
