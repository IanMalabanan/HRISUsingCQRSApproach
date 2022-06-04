using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HRIS.WPF.Core.Exceptions
{
    public class ServiceExp : Exception
    {
        public HttpStatusCode HttpCode { get; }
        public string Content { get; set; }
        public ServiceExp(string content)
        {
            Content = content;
        }
        public ServiceExp(string content, HttpStatusCode code)
        {
            Content = content;
            HttpCode = code;
        }
    }
}
