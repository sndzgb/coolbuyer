using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientExceptions
{
    /// <summary>
    /// This exception is thrown when an http request call fails (status code is in range 4xx & 5xx).
    /// </summary>
    public class ApiCallException : Exception
    {
        public string ErrorMessage { get; private set; }
        public int StatusCode { get; private set; }
        public Dictionary<string, string> Errors { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage">Response message from web api call.</param>
        /// <param name="statusCode">Status code that identifies the operation success or failure.</param>
        /// <param name="errors">Collection of additional errors that describe what caused the call to fail.</param>
        public ApiCallException(string errorMessage, int statusCode, Dictionary<string, string> errors)
        {
            this.ErrorMessage = errorMessage;
            this.StatusCode = statusCode;
            this.Errors = errors;
        }

        public ApiCallException()
            : base()
        {
        }

        public ApiCallException(string message)
            : base(message)
        {
        }

        public ApiCallException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ApiCallException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
