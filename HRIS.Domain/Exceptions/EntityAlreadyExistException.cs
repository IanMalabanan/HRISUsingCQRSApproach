using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Domain.Exceptions
{
    public class EntityAlreadyExistException : Exception
    {
        public EntityAlreadyExistException(string message) : base(message)
        {

        }
    }
}
