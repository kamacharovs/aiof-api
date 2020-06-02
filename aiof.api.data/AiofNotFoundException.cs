using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace aiof.api.data
{
    public class AiofNotFoundException : AiofFriendlyException
    {
        private const string DefaultMessage = "The requested item was not found.";

        public AiofNotFoundException()
            : base(HttpStatusCode.NotFound, DefaultMessage)
        { }

        public AiofNotFoundException(string message)
            : base(message)
        { }

        public AiofNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }

        public AiofNotFoundException(Exception inner)
            : base(DefaultMessage, inner)
        { }
    }
}
