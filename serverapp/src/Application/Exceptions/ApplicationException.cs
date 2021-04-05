using System;
using System.Collections.Generic;

namespace Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string error)
        {
            Errors = new string[] { error };
        }

        public ApplicationException(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }
    }
}

