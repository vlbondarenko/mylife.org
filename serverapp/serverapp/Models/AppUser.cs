using Microsoft.AspNetCore.Identity;

namespace serverapp.Models
{
    public class AppUser:IdentityUser
    {
        public string Nickname { get; set; }
    }
}
