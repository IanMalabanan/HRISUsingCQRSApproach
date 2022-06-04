using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.WPF.Core.Exceptions
{
    public class WebConnectFailureException : Exception
    {
        public WebConnectFailureException(string message) : base(message)
        {
        }

        public WebConnectFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
