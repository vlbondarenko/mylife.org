using System;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Data
{
    public class AppUser:IdentityUser
    {
        public DateTime? CreatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
