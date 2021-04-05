using System.Collections.Generic;

namespace Infrastructure.Identity.Exceptions
{
    public class NotFoundException:IdentityException
    {
        public NotFoundException(string error) : base(error) { }

        public NotFoundException(IEnumerable<string> errors) : base(errors) { }
    }
}
