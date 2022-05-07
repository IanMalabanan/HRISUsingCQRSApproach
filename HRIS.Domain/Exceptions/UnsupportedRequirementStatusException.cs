using System;

namespace HRIS.Domain.Exceptions
{
    public class UnsupportedRequirementStatusException : Exception
    {
        public UnsupportedRequirementStatusException(string code)
            : base($"PermitStatus is unsupported: {code}")
        {
        }
    }
}
