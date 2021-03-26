using System;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Data
{
    public class AppUser:IdentityUser
    {
        public DateTime? CreatedAt { get; set; }
    }
}
