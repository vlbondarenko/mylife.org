using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Persistence.Context;
using Persistence.Interfaces;

namespace Application.Tests.Common
{
    public static class DbContextFactory
    {
        private static readonly ApplicationDbContext _applicationDbContext;

        static DbContextFactory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestApplicationDatabase")
                .Options;

            _applicationDbContext = new ApplicationDbContext(options);

            _applicationDbContext.Database.EnsureCreated();
        }

        public static IUserProfileDbContext CreateUserProfileDbContext() => _applicationDbContext;
        
    }
}
