using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Common.Configuration.Messaging
{
    public class MessagingClientConfigBuilder
    {
        private string _baseUrl;
        private string _username;
        private string _password;
        private string _owner;
        private int _connectionTimeout = 30000;

        private readonly MessagingClientConfig _config;

        public MessagingClientConfigBuilder()
        {
            _baseUrl = "https://localhost:44398";
        }

        public MessagingClientConfigBuilder(MessagingClientConfig config)
        {
            _config = config;
            _baseUrl = _config.BaseUrl;
            _username = _config.Username;
            _password = _config.Password;
            _owner = _config.Owner;
        }

        public MessagingClientConfigBuilder SetBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        public MessagingClientConfigBuilder SetConnectionTimeout(int milliseconds)
        {
            _connectionTimeout = milliseconds;
            return this;
        }

        public MessagingClientConfigBuilder SetBasicAuthentication(string username, string password)
        {
            _username = username;
            _password = password;
            return this;
        }

        public MessagingClientConfig Build()
        {
            return new MessagingClientConfig()
            {
                BaseUrl = _baseUrl,
                ConnectionTimeout = _connectionTimeout,
                Username = _username,
                Password = _password,
                Owner = _owner
            };
        }
    }
}
