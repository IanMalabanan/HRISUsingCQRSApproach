using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Common.Configuration
{
    public class HttpClientProvider
    {
        public static HttpClient GetHttpClient(ClientConfig config, HttpClientHandler handler = null)
        {
            if (handler == null)
            {
                handler = new HttpClientHandler();
            }

            HttpClient client = new HttpClient(handler)
            {
                BaseAddress = new Uri(config.BaseUrl),
                Timeout = TimeSpan.FromMilliseconds(config.ConnectionTimeout),
                  
            };
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", config.GetAuthorizationHeader());

            //client.DefaultRequestHeaders.Add("Signature", config.CreateSignature());
            return client;
        }

    }
}
