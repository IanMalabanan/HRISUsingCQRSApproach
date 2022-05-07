using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Common.Configuration.Location
{
    public class LocationClientBuilder
    {
        private string _baseUrl_Fastrah;
        private string _baseUrl_IPInfo;
        private string _baseUrl_BigDataCloud;
        private string _apiKey;
        private string _apiKeyValue;
        private string _username;
        private string _password;
        private string _owner;
        private int _connectionTimeout = 30000;

        private readonly LocationClientConfig _config;

        public LocationClientBuilder()
        {
            _baseUrl_Fastrah = "https://ep.api.getfastah.com";
            _baseUrl_IPInfo = "http://ipinfo.io";
            _baseUrl_BigDataCloud = "https://api.bigdatacloud.net";
            _apiKey = "Fastah-Key";
            _apiKeyValue = "c59b2a802a9e44afa8064edacf76f959"; 
        }

        public LocationClientBuilder(LocationClientConfig config)
        {
            _config = config;
            _baseUrl_Fastrah = _config.BaseUrl_Fastrah;
            _baseUrl_IPInfo = _config.BaseUrl_IpInfo;
            _baseUrl_BigDataCloud = _config.BaseUrl_BigDataCloud;
            _apiKey = _config.APIKey;
            _apiKeyValue = _config.APIKeyValue;
            _username = _config.Username;
            _password = _config.Password;
            _owner = _config.Owner;
        }

        public LocationClientBuilder SetBaseUrl_Fastrah(string baseUrl)
        {
            _baseUrl_Fastrah = baseUrl;
            return this;
        }
        public LocationClientBuilder SetBaseUrl_IPInfo(string baseUrl)
        {
            _baseUrl_IPInfo = baseUrl;
            return this;
        }
        public LocationClientBuilder SetBaseUrl_BigDataCloud(string baseUrl)
        {
            _baseUrl_BigDataCloud = baseUrl;
            return this;
        }

        public LocationClientBuilder SetConnectionTimeout(int milliseconds)
        {
            _connectionTimeout = milliseconds;
            return this;
        }

        public LocationClientBuilder SetBasicAuthentication(string username, string password)
        {
            _username = username;
            _password = password;
            return this;
        }

        public LocationClientBuilder SetAPIKey(string key)
        {
            _apiKey = key;
            return this;
        }

        public LocationClientBuilder SetAPIKeyValue(string val)
        {
            _apiKeyValue = val;
            return this;
        }

        public LocationClientConfig Build()
        {
            return new LocationClientConfig()
            {
                BaseUrl_Fastrah = _baseUrl_Fastrah,
                BaseUrl_IpInfo = _baseUrl_IPInfo,
                BaseUrl_BigDataCloud = _baseUrl_BigDataCloud,
                APIKey = _apiKey,
                APIKeyValue = _apiKeyValue,
                ConnectionTimeout = _connectionTimeout,
                Username = _username,
                Password = _password,
                Owner = _owner
            };
        }
    }
}
