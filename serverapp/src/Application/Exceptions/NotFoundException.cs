using System.Collections.Generic;

namespace Application.Exceptions
{
    public class NotFoundException:ApplicationException
    {
        public NotFoundException(string error) : base(error) { }

        public NotFoundException(IEnumerable<string> errors) : base(errors) { }
    }
}
