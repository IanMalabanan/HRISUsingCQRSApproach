using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Application.Common.Exceptions
{
    public class InvalidTransactionStatusException : Exception
    {
        public InvalidTransactionStatusException(string message) : base(message)
        {

        }
    }
}
