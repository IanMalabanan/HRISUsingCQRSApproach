using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Services
{
    public class IdentityServerSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string IdentityBaseUrl { get; set; }
    }
}
