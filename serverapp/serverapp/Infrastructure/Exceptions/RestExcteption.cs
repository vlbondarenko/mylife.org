using System;
using System.Net;

namespace serverapp.Infrastructure
{
    public class RestExcteption:Exception
    {
        public RestExcteption (HttpStatusCode code, object error = null)
        {
            Code = code;
            Error = error;
        }

        public HttpStatusCode Code { get; set; }
        public object Error { get; set; }
    }
}
