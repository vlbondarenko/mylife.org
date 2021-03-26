using System;

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
