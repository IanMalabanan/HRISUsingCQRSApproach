using HRIS.Application.Common.Interfaces;
using HRIS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Services
{
    public class LocationService : ILocationService
    {
    //    private readonly HttpClient _httpClient;
    //    private LocationClientConfig _config;
    //    private IHttpContextAccessor _accessor;
    //    private readonly ILogger<LocationService> _logger;
    //    //create constructor and call HttpClient
    //    public LocationService(LocationClientBuilder builder, HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ILogger<LocationService> logger)
    //    {
    //        _config = builder.Build();

    //        _httpClient = httpClient;

    //        _accessor = httpContextAccessor;

    //        _logger = logger;
    //    }

    //    private async Task<string> GetIPAddress()
    //    {
    //        var ipAddress = await _httpClient.GetAsync(_config.BaseUrl_IpInfo + "/ip");

    //        if (ipAddress.IsSuccessStatusCode)
    //        {
    //            var json = await ipAddress.Content.ReadAsStringAsync();
    //            return json.ToString();
    //        }
    //        return "";
    //    }

    //    public async Task<LocationVM> GetClientIPAddress()
    //    {
            
    //        if (_accessor.HttpContext == null)
    //        {
    //            throw new ApplicationException("Unable to retrieve httpcontext");
    //        }

    //        var fetchIP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString().Replace("{", "").Replace("}", "");

    //        LocationVM data = new LocationVM();

    //        if (!string.IsNullOrEmpty(fetchIP))
    //        {
    //            if (fetchIP == "::1" || fetchIP == "127.0.0.1" || fetchIP == "127.0.0.0")
    //            {
    //                data.ip = await GetIPAddress();
    //            }
    //            else
    //            {
    //                data.ip = fetchIP;
    //            }
    //        }
    //        else
    //        {
    //            data.ip = await GetIPAddress();
    //        }

    //        return data;
            
            
    //    }

    //    public async Task<LocationVM> GetLocation()
    //    {
    //        string ipAddress = string.Empty;

    //        var fetchIP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString().Replace("{","").Replace("}","");

    //        if (_accessor.HttpContext == null)
    //        {
    //            throw new NullReferenceException("Unable to retrieve httpcontext");
    //        }

    //        if (!string.IsNullOrEmpty(fetchIP))
    //        {
    //            if (fetchIP == "::1" || fetchIP == "127.0.0.1" || fetchIP == "127.0.0.0")
    //            {
    //                ipAddress = await GetIPAddress();
    //            }
    //            else
    //            {
    //                ipAddress = fetchIP;
    //            }
    //        }
    //        else
    //        {
    //            ipAddress = await GetIPAddress();
    //        }

    //        string jsonResult = string.Empty;

    //        string endpoint = "/whereis/v1/json/" + ipAddress;

    //        _httpClient.DefaultRequestHeaders.Clear();
    //        _httpClient.DefaultRequestHeaders.Add(_config.APIKey,_config.APIKeyValue);
    //        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //        var responseMessage = await _httpClient.GetAsync(_config.BaseUrl_Fastrah + endpoint).ConfigureAwait(false);

    //        jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

    //        if (responseMessage.IsSuccessStatusCode)
    //        {
    //            var model = new LocationVM();

    //            model = JsonConvert.DeserializeObject<LocationVM>(jsonResult);

    //            return model;
    //        }
    //        else
    //        {
    //            throw new ApplicationException(responseMessage.StatusCode.ToString(), new Exception(jsonResult));
    //        }
    //    }

    //    public async Task<LocationVM> GetLocationFromAnotherIPAddress(string ipAddress)
    //    {
    //        string jsonResult = string.Empty;

    //        string endpoint = "/whereis/v1/json/" + ipAddress;

    //        _httpClient.DefaultRequestHeaders.Clear();
    //        _httpClient.DefaultRequestHeaders.Add(_config.APIKey, _config.APIKeyValue);
    //        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //        var responseMessage = await _httpClient.GetAsync(_config.BaseUrl_Fastrah + endpoint).ConfigureAwait(false);

    //        jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

    //        if (responseMessage.IsSuccessStatusCode)
    //        {
    //            var model = new LocationVM();

    //            model = JsonConvert.DeserializeObject<LocationVM>(jsonResult);

    //            return model;
    //        }
    //        else
    //        {
    //            throw new ApplicationException(responseMessage.StatusCode.ToString(), new Exception(jsonResult));
    //        }
    //    }

    //    public async Task<LocationVM> GetAddressByLatitudeAndLongitude(decimal latitude, decimal longitude)
    //    {
    //        string jsonResult = string.Empty;

    //        string endpoint = "/data/reverse-geocode-client?latitude=" + latitude + "&longitude=" + longitude + "&localityLanguage=en";

    //        var responseMessage = await _httpClient.GetAsync(_config.BaseUrl_BigDataCloud + endpoint);

    //        jsonResult = await responseMessage.Content.ReadAsStringAsync();

    //        if (responseMessage.IsSuccessStatusCode)
    //        {
    //            var model = new LocationVM();

    //            model = JsonConvert.DeserializeObject<LocationVM>(jsonResult);

    //            return model;
    //        }
    //        else
    //        {
    //            throw new ApplicationException(responseMessage.StatusCode.ToString(), new Exception(jsonResult));
    //        }
    //    }
    }
}

