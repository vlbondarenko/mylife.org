using System;
using System.Linq;
using System.Collections.Generic;
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
