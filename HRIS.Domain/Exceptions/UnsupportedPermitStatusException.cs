
using System;

namespace HRIS.Domain.Exceptions
{
    public class UnsupportedPermitStatusException : Exception
    {
        public UnsupportedPermitStatusException(string code)
            : base($"PermitStatus is unsupported: {code}")
        {
        }
    }
}
