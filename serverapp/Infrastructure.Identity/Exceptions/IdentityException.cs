using System;

namespace Infrastructure.Identity.Exceptions
{
    public class IdentityException:Exception
    {
        public IdentityException()
        {

        }

        public string ErrorMessage { get; set; }
    }
}
