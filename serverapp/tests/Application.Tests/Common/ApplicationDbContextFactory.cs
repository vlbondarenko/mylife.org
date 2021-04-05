using System;

using Microsoft.EntityFrameworkCore;

using Persistence.Context;
using Domain.Entities;

namespace Application.Tests.Common
{
    public static class ApplicationDbContextFactory
    {

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();
            try
            {
                context.UserProfiles.Add(new UserProfile 
                { 
                    Id = "00000000-0000-0000-0000-000000000000", 
                    FirstName = "Lol", 
                    LastName = "Kek" ,
                    BirthDate = DateTime.Now,
                    City = "Sinyavka",
                    Country = "Russia"
                });
                context.SaveChanges();
            }
            catch
            {
                //TODO:add logging
            }
          
            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
        
    }
}
