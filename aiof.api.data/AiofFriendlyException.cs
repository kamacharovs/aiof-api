using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace aiof.api.data
{
    public class AiofFriendlyException : AiofException
    {
        public AiofFriendlyException()
        { }

        public AiofFriendlyException(string message)
            : base(message)
        { }

        public AiofFriendlyException(HttpStatusCode statusCode, string message)
            : base(statusCode, message)
        { }

        public AiofFriendlyException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
