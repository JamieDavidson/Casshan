using System;
using System.Net;

namespace Casshan.Service.Exceptions
{
    internal sealed class NonSuccessResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public NonSuccessResponseException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
