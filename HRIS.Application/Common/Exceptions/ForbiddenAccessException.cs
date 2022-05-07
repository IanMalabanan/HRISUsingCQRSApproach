using System;

namespace HRIS.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() { }
        public string Policy { get; set; }
    }
}
