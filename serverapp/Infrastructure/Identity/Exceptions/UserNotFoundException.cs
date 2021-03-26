using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Exceptions
{
    class UserNotFoundException:Exception
    {
        public UserNotFoundException (string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}
