using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HRIS.WPF.Core.Exceptions
{
    public class WarningExp : Exception
    {
        public WarningExp(string message) : base(message)
        {
        }
    }
}
