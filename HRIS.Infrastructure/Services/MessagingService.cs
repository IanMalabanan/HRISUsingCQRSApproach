using HRIS.Application.Common.Interfaces;
using HRIS.Domain.ViewModels;
using HRIS.Infrastructure.Common.Configuration.Messaging;
//using HRIS.Infrastructure.Api.Messaging;
using HRIS.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Services
{
    public static class MessagingServiceExtensions
    {
        public static void FrebasService(this IServiceCollection services, IConfiguration configuration)
        {
            //https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient<IMessagingService, MessagingService>();
        }
    }


    public class MessagingService : ApiServiceBase, IMessagingService
    {

        //ITokenAccessorService tokenAccessorService, tokenAccessorService,
        private MessagingClientConfig _config;


        public MessagingService(MessagingClientConfigBuilder builder, HttpClient _httpClient) : base(null, _httpClient)
        {
            _config = builder.Build();
        }

        public async Task<EmailResponseModel> SendTestEmail(string fromEmail, string toEmail, string emailBody)
        {
            var _sendEmailURL = "api/InfoBip/sendwithpayload?owner=" + _config.Owner;

            InfobipEmailMessageModel _email = new InfobipEmailMessageModel();

            _email.From = fromEmail;
            _email.To = toEmail;
            _email.Format = "Html";
            _email.Subject = "Test Email";
            _email.Body = emailBody;

            var _url = _config.BaseUrl + _sendEmailURL;
            var _results = await base.PostAsync<InfobipEmailMessageModel, EmailResponseModel>(_url, _email);

            return _results;
        }


        public override async Task PrepareAuthenticatedClient()
        {
            //var _accessToken = await _tokenAccessorService.GetAccessTokenAsync();

            var _accessToken = "";
            using (var httpClient = new HttpClient())
            {
                var creds = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("username", _config.Username),
                    new KeyValuePair<string, string>("password", _config.Password)
                };

                var content = new FormUrlEncodedContent(creds);

                using (var response = await httpClient.PostAsync(_config.BaseUrl.TrimEnd('/') + "/token?" + "owner=" + _config.Owner, content))
                {
                    _accessToken = await response.Content.ReadAsStringAsync();
                }
            }

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<EmailResponseModel> SendEmail(string subject, string fromEmail, string toEmail, string emailBody)
        {
            try
            {
                var _sendEmailURL = "api/InfoBip/sendwithpayload?owner=" + _config.Owner;

                InfobipEmailMessageModel _email = new InfobipEmailMessageModel();

                _email.From = fromEmail;
                _email.To = toEmail;
                _email.Format = "Html";
                _email.Subject = subject;
                _email.Body = emailBody;

                var _url = _config.BaseUrl + _sendEmailURL;

                var _results = await base.PostAsync<InfobipEmailMessageModel, EmailResponseModel>(_url, _email);

                return _results;
            }
            catch(Exception e)
            {
                return null;
            }
           
        }
    }
}
