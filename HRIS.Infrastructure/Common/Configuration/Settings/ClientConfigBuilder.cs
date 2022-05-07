using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Common.Configuration
{
    public class ClientConfigBuilder : ClientConfig
    {
        private string _baseUrl;
        private string _username;
        private string _password;
        private string _owner;
        private int _connectionTimeout = 3000000;
        private string _dataForEncryption;

        public ClientConfigBuilder()
        {
            _baseUrl = BaseUrl;
            _username = Username;
            _password = Password;
            _owner = Owner;
            _dataForEncryption = dataForEncryption;
        }

        public ClientConfigBuilder SetBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        public ClientConfigBuilder SetConnectionTimeout(int milliseconds)
        {
            _connectionTimeout = milliseconds;
            return this;
        }

        public ClientConfigBuilder SetBasicAuthentication(string username, string password)
        {
            _username = username;
            _password = password;
            return this;
        }

        public ClientConfigBuilder SetdataForEncryption(string dataForEncryption)
        {
            _dataForEncryption = dataForEncryption;
            return this;
        }



        public ClientConfig Build()
        {
            return new ClientConfig()
            {
                BaseUrl = _baseUrl,
                ConnectionTimeout = _connectionTimeout,
                Username = _username,
                Password = _password,
                Owner = _owner,
                dataForEncryption = _dataForEncryption
            };
        }
    }
}
