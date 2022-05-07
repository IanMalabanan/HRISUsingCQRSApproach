using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Common.Configuration.Location
{
    public class LocationClientConfig
    {
        public string BaseUrl_IpInfo { get; set; } = "http://ipinfo.io";
        public string BaseUrl_Fastrah { get; set; } = "https://ep.api.getfastah.com";
        public string BaseUrl_BigDataCloud { get; set; } = "https://api.bigdatacloud.net";
        
        public int ConnectionTimeout { get; set; } = 20000;
        public int ReadTimeout { get; set; } = 20000;
        public string Username { get; set; }
        public string Password { get; set; }
        public string APIKey { get; set; } = "Fastah-Key";
        public string APIKeyValue { get; set; } = "c59b2a802a9e44afa8064edacf76f959";
        public string Owner { get; set; }
        public string[] Scopes { get; set; } = new string[] { };



    }
}
