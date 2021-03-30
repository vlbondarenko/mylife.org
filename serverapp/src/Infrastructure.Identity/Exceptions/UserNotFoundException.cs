﻿using System.Collections.Generic;

namespace Infrastructure.Identity.Exceptions
{
    public class UserNotFoundException:IdentityException
    {
        public UserNotFoundException(string error) : base(error) { }

        public UserNotFoundException(IEnumerable<string> errors) : base(errors) { }
    }
}