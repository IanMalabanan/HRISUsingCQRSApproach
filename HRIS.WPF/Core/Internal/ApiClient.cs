using HRIS.WPF.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HRIS.WPF.Core.Internal
{
    internal class ApiClient : IApiRepository
    {
        public string AuthTokenType { get; set; }
        public string AuthToken { get; set; }

        private bool _isThrowErrorWhenUnauthorized = true;

        public void DisableThrowErrorWhenUnauthorized()
        {
            _isThrowErrorWhenUnauthorized = false;
        }

        private bool IsTransformed<TR>(TR value)
        {
            if (!value.GetType().IsClass) return true;

            PropertyInfo[] properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in properties)
            {
                var item = p.GetValue(value);

                if (item != null) { return true; }
            }

            return false;
        }

        public async Task<ApiResponse> GetAsync(string uri)
        {
            HttpClient httpClient = CreateHttpClient();
            string _reponseContent = string.Empty;
            httpClient.Timeout = TimeSpan.FromMinutes(10);

            var _httpReponse = await httpClient.GetAsync(uri);
            _reponseContent = await _httpReponse.Content.ReadAsStringAsync();

            if (_isThrowErrorWhenUnauthorized && _httpReponse.StatusCode == HttpStatusCode.Unauthorized)
                throw new ApiClientException(_httpReponse.StatusCode, uri, "Request returns Unathorized", _reponseContent);

            return new ApiResponse()
            {
                StatusCode = _httpReponse.StatusCode,
                Content = _reponseContent,
                IsSuccessStatusCode = _httpReponse.IsSuccessStatusCode
            };
        }

        public async Task<ApiResponse> GetAsync(string uri, bool allowanonymous)
        {
            HttpClient httpClient = CreateHttpClient();
            string _reponseContent = string.Empty;
            httpClient.Timeout = TimeSpan.FromMinutes(10);
            if (allowanonymous)
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
            }

            var _httpReponse = await httpClient.GetAsync(uri);
            _reponseContent = await _httpReponse.Content.ReadAsStringAsync();

            if (_isThrowErrorWhenUnauthorized && _httpReponse.StatusCode == HttpStatusCode.Unauthorized)
                throw new ApiClientException(_httpReponse.StatusCode, uri, "Request returns Unathorized", _reponseContent);

            return new ApiResponse()
            {
                StatusCode = _httpReponse.StatusCode,
                Content = _reponseContent,
                IsSuccessStatusCode = _httpReponse.IsSuccessStatusCode
            };
        }

        public async Task<ApiResponse> GetAsync<T>(string uri, T data)
        {
            try
            {
                var _queryString = string.Empty;//UrlHelper.TransformToQueryString(data);
                var _url = uri.TrimEnd('/') + "?" + _queryString;

                HttpClient httpClient = CreateHttpClient();
                string _reponseContent = string.Empty;
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                var _httpReponse = await httpClient.GetAsync(_url);
                _reponseContent = await _httpReponse.Content.ReadAsStringAsync();

                if (_isThrowErrorWhenUnauthorized && _httpReponse.StatusCode == HttpStatusCode.Unauthorized)
                    throw new ApiClientException(_httpReponse.StatusCode, uri, "Request returns Unathorized", _reponseContent);

                return new ApiResponse()
                {
                    StatusCode = _httpReponse.StatusCode,
                    Content = _reponseContent,
                    IsSuccessStatusCode = _httpReponse.IsSuccessStatusCode
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ApiResponse> PostAsync<T>(string uri, T data)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string _reponseContent = string.Empty;

                var _httpReponse = await httpClient.PostAsync(uri, content);
                _reponseContent = await _httpReponse.Content.ReadAsStringAsync();

                if (_isThrowErrorWhenUnauthorized && _httpReponse.StatusCode == HttpStatusCode.Unauthorized)
                    throw new ApiClientException(_httpReponse.StatusCode, uri, "Request returns Unathorized", _reponseContent);

                return new ApiResponse()
                {
                    StatusCode = _httpReponse.StatusCode,
                    Content = _reponseContent,
                    IsSuccessStatusCode = _httpReponse.IsSuccessStatusCode
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ApiResponse> PostFormUrlEncodedContentAsync(string uri, IEnumerable<KeyValuePair<string, string>> data)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                var content = new FormUrlEncodedContent(data);
                
                string _reponseContent = string.Empty;

                var _httpReponse = await httpClient.PostAsync(uri, content);
                _reponseContent = await _httpReponse.Content.ReadAsStringAsync();

                if (_isThrowErrorWhenUnauthorized && _httpReponse.StatusCode == HttpStatusCode.Unauthorized)
                    throw new ApiClientException(_httpReponse.StatusCode, uri, "Request returns Unathorized", _reponseContent);

                return new ApiResponse() {
                    StatusCode = _httpReponse.StatusCode,
                    Content = _reponseContent,
                    IsSuccessStatusCode = _httpReponse.IsSuccessStatusCode
                };

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<TR> PostAsync<T, TR>(string uri, T data)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string _reponseContent = string.Empty;

                var _httpReponse = await httpClient.PostAsync(uri, content);

                _reponseContent = await _httpReponse.Content.ReadAsStringAsync();

                if (_httpReponse.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<TR>(_reponseContent);
                    if (!IsTransformed(json))
                        throw new ApiClientException(_httpReponse.StatusCode, uri, "APIClient Error: Unable to deserialize object", _reponseContent);
                    return json;
                }

                throw new ApiClientException(_httpReponse.StatusCode, uri, "APIClient Error", _reponseContent);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> PutAsync<T>(string uri, T data)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string _reponseContent = string.Empty;

                var _httpReponse = await httpClient.PutAsync(uri, content);

                _reponseContent = await _httpReponse.Content.ReadAsStringAsync();

                if (_httpReponse.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<T>(_reponseContent);
                    return json;
                }

                var _apiError = JsonConvert.DeserializeObject<ApiError>(_reponseContent);
                throw new ApiClientException(_httpReponse.StatusCode, uri, _apiError?.Error ?? _reponseContent);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<TR> PutAsync<T, TR>(string uri, T data)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string _reponseContent = string.Empty;

                var _httpReponse = await httpClient.PutAsync(uri, content);
                _reponseContent = await _httpReponse.Content.ReadAsStringAsync();

                if (_httpReponse.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<TR>(_reponseContent);
                    return json;
                }

                var _apiError = JsonConvert.DeserializeObject<ApiError>(_reponseContent);
                throw new ApiClientException(_httpReponse.StatusCode, uri, _apiError?.Error ?? _reponseContent);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task DeleteAsync(string uri)
        {
            HttpClient httpClient = CreateHttpClient();
            await httpClient.DeleteAsync(uri);
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(120);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(AuthToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthTokenType, AuthToken);

            return httpClient;
        }

        public Task<string> UploadImageAsync(Stream image, string fileName, string uri)
        {
            throw new NotImplementedException();
        }

        
    }
}
