using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

using System.Net.Http;
using System.Text;

using Infrastructure.Identity.Data;
using Persistence.Context;
using Domain.Entities;

namespace WebApi.Tests.Common
{
    public static class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTest(IdentityDbContext identityDbContext, ApplicationDbContext applicationDbContext)
        {
            var email = "test@test.com";
            var userName = "test";

            var appUser = new AppUser()
            {
                Email = email,
                UserName = userName,
                NormalizedEmail = email.Normalize().ToUpperInvariant(),
                NormalizedUserName = userName.Normalize().ToUpperInvariant(),
                PasswordHash = "AQAAAAEAACcQAAAAEEcMpYe1+501CVRMdMPxzPgc/VAzvYg6ql2pB3Rex9Lq3qyzcN5+QSSW2sRLRwqbAw==" //hash for "testpassword"
            };

            identityDbContext.Users.Add(appUser);
            identityDbContext.SaveChanges();

            var userPrifile = new UserProfile()
            {
                Id = appUser.Id,
                FirstName = "Firstname",
                LastName = "Lastname",
                BirthDate = DateTime.Now,
                City = "Dushambe",
                Country = "USA"
            };

            applicationDbContext.UserProfiles.Add(userPrifile);
            applicationDbContext.SaveChanges();
        }
    }
}
