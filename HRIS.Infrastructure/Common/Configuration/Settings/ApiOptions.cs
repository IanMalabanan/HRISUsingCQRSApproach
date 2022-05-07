using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Common.Configuration
{
    public class ApiOptions
    {
        public string IdentityServerBaseUrl { get; set; }
        public string OidcApiName { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}
