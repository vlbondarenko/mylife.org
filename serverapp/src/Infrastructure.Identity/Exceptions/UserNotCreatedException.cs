using System;
using System.Collections.Generic;

namespace Infrastructure.Identity.Exceptions
{
    public class UserNotCreatedException:IdentityException
    {
        public UserNotCreatedException(string error) : base(error) { }

        public UserNotCreatedException(IEnumerable<string> errors) : base(errors) { }
       
    }
}
