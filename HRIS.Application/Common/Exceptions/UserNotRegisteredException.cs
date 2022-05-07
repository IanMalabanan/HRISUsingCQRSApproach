using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Application.Common.Exceptions
{
    public class UserNotRegisteredException : Exception
    {
        public UserNotRegisteredException(string message) : base(message)
        {

        }
    }
}
