using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Application.Common.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message): base(message)
        {

        }
    }
}
