using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Domain.Exceptions
{
    public class UnsatisfiedRequiredFieldsException : Exception
    {
        public UnsatisfiedRequiredFieldsException(string message) : base(message)
        {

        }
    }
}
