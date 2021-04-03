using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string error)
        {
            Errors = new string[] { error };
        }

        public NotFoundException(IEnumerable<string> errors)
        {
            _ = (errors is null) ? Errors = new string[] { } : Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
