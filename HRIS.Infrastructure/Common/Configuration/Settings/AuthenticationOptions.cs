using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Infrastructure.Common.Configuration
{
    public class AuthenticationOptions
    {
        public string IdentityRedirectUri { get; set; }
        public string IdentityBaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }        
        public string TokenValidationClaimName { get; set; }
        public string TokenValidationClaimRole { get; set; }
        public bool RequireHttpsMetadata { get; set; }        
        public string OidcResponseType { get; set; }
        public bool UsePkce { get; set; }
        public string CookieName { get; set; }
        public double CookieExpiresUtcHours { get; set; }
        public string[] Scopes { get; set; }
        public string SignedOutCallbackPath { get; internal set; }
    }
}
