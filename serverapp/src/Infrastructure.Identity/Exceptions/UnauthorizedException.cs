using System.Collections.Generic;

namespace Infrastructure.Identity.Exceptions
{
    public class UnauthorizedException:IdentityException
    {
        public UnauthorizedException(string error) : base(error) { }

        public UnauthorizedException(IEnumerable<string> errors) : base(errors) { }
    }
}
