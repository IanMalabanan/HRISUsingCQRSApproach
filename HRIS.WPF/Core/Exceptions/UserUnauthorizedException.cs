using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.WPF.Core.Exceptions
{
    public class UserUnauthorizedException : Exception
    {
        public UserUnauthorizedException() : base("User Unauthorized")
        {
        }

        public UserUnauthorizedException(string message) : base(message)
        {
        }
    }
}
