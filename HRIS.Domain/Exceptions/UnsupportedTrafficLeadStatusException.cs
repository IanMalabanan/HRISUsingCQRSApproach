
using System;

namespace HRIS.Domain.Exceptions
{
    public class UnsupportedTrafficLeadStatusException : Exception
    {
        public UnsupportedTrafficLeadStatusException(string code)
            : base($"Traffic Lead  Status is unsupported: {code}")
        {
        }
    }
}
