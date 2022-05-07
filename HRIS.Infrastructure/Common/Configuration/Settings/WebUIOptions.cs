using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Infrastructure.Common.Configuration
{
    public class WebUIOptions
    {
        public ServiceApiOptions ServiceApi { get; set; }
        public AuthenticationOptions Authentication { get; set; }
    }
}
