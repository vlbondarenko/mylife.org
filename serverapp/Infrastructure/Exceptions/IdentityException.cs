using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class IdentityException:Exception
    {
        public IdentityException()
        {

        }

        public string ErrorMessage { get; set; }
    }
}
