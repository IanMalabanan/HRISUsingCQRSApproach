using HRIS.Application.Common.Interfaces;
using HRIS.Application.Common.Interfaces.Application;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Services
{
    public class TokenAccessorService : ITokenAccessorService
    {
        private readonly ILogger<TokenAccessorService> _logger;
        private readonly IOptions<IdentityServerSettings> _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoveryDocument;
        public TokenAccessorService(ILogger<TokenAccessorService> logger, IOptions<IdentityServerSettings> identityServerSettings)
        {
            _logger = logger;
            _identityServerSettings = identityServerSettings;

            using var httpClient = new HttpClient();

            _discoveryDocument = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.Value.IdentityBaseUrl).Result;

            if (_discoveryDocument.IsError)
            {
                throw new Exception("Error");
            }
        }

        public async Task<string> GetAccessTokenAsync()
        {
            using var client = new HttpClient();

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,
                ClientId = _identityServerSettings.Value.ClientId,
                ClientSecret = _identityServerSettings.Value.ClientSecret,
                Scope = "payment"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception("Error");
            }

            return tokenResponse.AccessToken;
        }
    }
}
