using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Exceptions
{
    class UserNotCreatedException:Exception
    {
        public UserNotCreatedException(string error)
        {
            Errors = new string[] { error };
        }

        public UserNotCreatedException (IEnumerable<string> errors)
        {
            if (errors is not null)
            {
                Errors = errors;
            }
            else
            {
                Errors = new string[] { };
            }
        }

        public IEnumerable<string> Errors { get; }
    }
}
