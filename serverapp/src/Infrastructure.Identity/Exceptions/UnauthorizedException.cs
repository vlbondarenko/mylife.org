using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Exceptions
{
    public class UnauthorizedException:IdentityException
    {
        public UnauthorizedException(string error) : base(error) { }

        public UnauthorizedException(IEnumerable<string> errors) : base(errors) { }
    }
}
