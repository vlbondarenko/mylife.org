using System;
using System.Collections.Generic;

namespace Infrastructure.Identity.Exceptions
{
    public class IdentityException:Exception
    {
        public IdentityException(string error)
        {
            Errors = new string[] { error };
        }

        public IdentityException (IEnumerable<string> errors)
        {
            _ = (errors is null) ? Errors = new string[] { } : Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
