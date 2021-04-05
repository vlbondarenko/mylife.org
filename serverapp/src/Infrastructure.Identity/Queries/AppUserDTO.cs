using System;

namespace Infrastructure.Identity.Queries
{
    public class AppUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string AccessToken { get; set; }
    }
}
