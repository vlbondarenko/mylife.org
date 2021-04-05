using System.Collections.Generic;

namespace Infrastructure.Identity.Exceptions
{
    public class NotCreatedException:IdentityException
    {
        public NotCreatedException(string error) : base(error) { }

        public NotCreatedException(IEnumerable<string> errors) : base(errors) { }
       
    }
}
