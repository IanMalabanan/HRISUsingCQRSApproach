using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Services
{
    public class ApiServiceBase
    {
        private readonly ITokenAccessorService _tokenAccessorService;
        protected readonly HttpClient HttpClient;

        public ApiServiceBase(ITokenAccessorService tokenAccessorService, HttpClient httpClient)
        {
            _tokenAccessorService = tokenAccessorService;
            HttpClient = httpClient;
        }

        public async virtual Task PrepareAuthenticatedClient()
        {
            var _accessToken = await _tokenAccessorService.GetAccessTokenAsync();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task ThrowAPIErrorException(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Unauthorized");
            
            var _errorDetails = await response.Content.ReadFromJsonAsync<ApiError>();

            if (_errorDetails.Status == StatusCodes.Status400BadRequest)
                if (_errorDetails.Error?.Any() ?? false)
                    throw new ValidationException(_errorDetails.Error);
                else
                    throw new ValidationException();

            else if (_errorDetails.Status == StatusCodes.Status404NotFound)
                throw new NotFoundException(_errorDetails.Detail);

            else if (_errorDetails.Status == StatusCodes.Status403Forbidden)
                throw new ForbiddenAccessException();

            else
                throw new Exception("API Error: " + response.RequestMessage.RequestUri.AbsoluteUri);
        }

        protected async Task<TValue> PostAsync<TValue>(string url, TValue model)
        {
            return await PostAsync<TValue, TValue>(url, model);
        }

        protected async Task<TOutput> PostAsync<TValue, TOutput>(string url, TValue model)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.PostAsJsonAsync(url, model);

            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);

            var _results = await _response.Content.ReadFromJsonAsync<TOutput>();
            return _results;
        }

        protected async Task DeleteAsync(string url)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.DeleteAsync(url);
            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);
            _response.EnsureSuccessStatusCode();
        }        

        protected async Task<TValue> GetAsync<TValue>(string url, bool useDefaultOnNotFound)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.GetAsync(url);

            if (_response.StatusCode == System.Net.HttpStatusCode.NotFound && useDefaultOnNotFound)
                return default(TValue);

            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);

            var _results = await _response.Content.ReadFromJsonAsync<TValue>();
            return _results;
        }

        protected async Task<TValue> GetAsync<TValue>(string url)
        {
            return await GetAsync<TValue>(url, false);
        }

        protected async Task<TOutput> PutAsync<TValue, TOutput>(string url, TValue model)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.PutAsJsonAsync(url, model);
            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);
            var _results = await _response.Content.ReadFromJsonAsync<TOutput>();
            return _results;
        }

        protected async Task<TValue> PutAsync<TValue>(string url, TValue model)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.PutAsJsonAsync(url, model);
            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);
            var _results = await _response.Content.ReadFromJsonAsync<TValue>();
            return _results;
        }
    }
}
