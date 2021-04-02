using Newtonsoft.Json;

using System.Net.Http;
using System.Text;

using Infrastructure.Identity.Data;

namespace WebApi.Tests.Common
{
    public static class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static void InitializeIdentityDbForTest(IdentityDbContext context)
        {
            var email = "test@test.com";
            var userName = "test";

            context.Users.Add(new AppUser()
            {
                Email = email,
                UserName = userName,
                NormalizedEmail = email.Normalize().ToUpperInvariant(),
                NormalizedUserName = userName.Normalize().ToUpperInvariant()
            });

            context.SaveChanges();
        }
    }
}
