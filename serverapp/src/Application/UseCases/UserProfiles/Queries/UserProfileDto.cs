using System;

namespace Application.UseCases.UserProfiles.Queries
{
    public class UserProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
