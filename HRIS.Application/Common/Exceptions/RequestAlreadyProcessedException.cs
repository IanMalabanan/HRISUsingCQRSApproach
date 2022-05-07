using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Application.Common.Exceptions
{
    public class RequestAlreadyProcessedException :Exception
    {
        public RequestAlreadyProcessedException(string message) : base(message) { }
    }
}
