using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace aiof.api.data
{
    public abstract class AiofException : ApplicationException
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; }

        protected AiofException()
        { }

        protected AiofException(int statusCode)
        {
            StatusCode = statusCode;
        }

        protected AiofException(string message)
            : base(message)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        protected AiofException(string message, Exception inner)
            : base(message, inner)
        { }

        protected AiofException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        protected AiofException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = (int)statusCode;
        }

        protected AiofException(int statusCode, Exception inner)
            : this(statusCode, inner.ToString())
        { }

        protected AiofException(HttpStatusCode statusCode, Exception inner)
            : this(statusCode, inner.ToString())
        { }

        protected AiofException(int statusCode, JsonElement errorObject)
            : this(statusCode, errorObject.ToString())
        {
            ContentType = @"application/problem+json";
        }
    }
}
