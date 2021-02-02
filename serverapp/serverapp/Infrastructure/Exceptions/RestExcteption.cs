using System;
using System.Net;

namespace serverapp.Infrastructure
{
    public class RestExcteption:Exception
    {
        public RestExcteption (HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }

        public HttpStatusCode Code { get; set; }
        public object Errors { get; set; }
    }
}
