using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Common.Exceptions
{
    public class ValidationException:Exception
    {
        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures.Select(failure => failure.ErrorMessage);
        }

        public IEnumerable<string> Errors { get; }
    } 
}
