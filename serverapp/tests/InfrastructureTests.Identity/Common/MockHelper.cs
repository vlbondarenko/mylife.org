﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

namespace Infrastructure.Identity.Tests.Common
{
    public static class MockHelper
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser> (IUserStore<TUser> userStore = null) where TUser : class
        {
            var store = userStore ?? new Mock<IUserStore<TUser>>().Object;
            var mockUserMngr = new Mock<UserManager<TUser>>(store, null, null, null, null, null, null, null, null);
            return mockUserMngr;
        }
    }
}
