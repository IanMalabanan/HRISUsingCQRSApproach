using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Common.Configuration
{
    public class ClientConfig
    {
        public string BaseUrl { get; set; }
        public int ConnectionTimeout { get; set; } = 20000;
        public int ReadTimeout { get; set; } = 20000;
        public string Username { get; set; }
        public string Password { get; set; }
        public string Owner { get; set; }

        public string dataForEncryption { get; set; }

        public string[] Scopes { get; set; } = new string[] { };

        public string GetAuthorizationHeader()
        {
            return "Basic " + Base64Credentials();
        }

        private string Base64Credentials()
        {
            return Convert.ToBase64String(
                Encoding.ASCII.GetBytes(Username + ":" + Password)
            );
        }

        //public string CreateSignature()
        //{
        //    return HashSignature.GenerateSignature(dataForEncryption);
        //}
    }
}
