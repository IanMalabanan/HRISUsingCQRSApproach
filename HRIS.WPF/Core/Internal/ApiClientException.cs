using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HRIS.WPF.Core.Internal
{
    public class ApiClientException : Exception
    {
        public HttpStatusCode HttpCode { get; }
        public string Url { get; }
        public string ResponseBody { get; set; }

        public ApiClientException(HttpStatusCode code) : this(code, null, null)
        {
        }

        public ApiClientException(HttpStatusCode code, string message) : this(code, null, message, null)
        {
        }

        public ApiClientException(HttpStatusCode code, string url, string message) : this(code, url, message, null)
        {
        }

        public ApiClientException(HttpStatusCode code, string url, string message, string responseBody) : this(code, url, message, responseBody, null)
        {
        }

        public ApiClientException(HttpStatusCode code, string url, string message, string responseBody, Exception inner) : base(message, inner)
        {
            HttpCode = code;
            Url = url;
            ResponseBody = responseBody;
        }
    }
}
