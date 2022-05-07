using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Application.Common.Exceptions
{
    public class PaymentRequestExpiredException : Exception
    {
        public PaymentRequestExpiredException(string message) : base(message)
        {

        }
    }
}
