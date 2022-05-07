using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Application.Common.Exceptions
{
    public class InvalidConversionException : Exception
    {
        public InvalidConversionException(string message) : base(message) { }
    }
}
