using Hcom.App.Services.Core;
using HRIS.WPF.Core.Exceptions;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HRIS.WPF.Core
{

    public class CoreRepository : IRepository
    {
        public CoreRepository()
        {
           
        }
        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);
                string jsonResult = string.Empty;
                string customHeader = await GetCustomRequestHeader();
                httpClient.DefaultRequestHeaders.Add("clientInfo", customHeader);
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.GetAsync(uri).ConfigureAwait(false));

                jsonResult =
                  await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Debug.WriteLine($"{"Status : " + responseMessage.StatusCode }");
                    //await _dialogService.ShowAlertAsync("Session Expired, Please relogin.", "Project list", "OK");
                    //await NavigationService.NavigateToAsync<LoginViewModel>();
                    //await NavigationService.RemoveBackStackAsync();
                    throw new HttpExp(responseMessage.StatusCode);
                }

                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ApplicationException(jsonResult.ToString().Replace("\"", ""));
                }


                throw new HttpExp(responseMessage.StatusCode);


            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                //return (T)Convert.ChangeType(null, typeof(T));
                throw;
            }
        }

        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage =
                    await Policy
                    .Handle<ApplicationException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () =>
                    await httpClient.PostAsync(uri, content)
                    )
                    ;


                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //await _dialogService.ShowAlertAsync("Session Expired, Please relogin.", "Project list", "OK");
                    //await NavigationService.NavigateToAsync<LoginViewModel>();
                    //await NavigationService.RemoveBackStackAsync();
                    throw new ServiceExp(jsonResult, responseMessage.StatusCode);
                }

                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ApplicationException(jsonResult.ToString().Replace("\"", ""));
                }

                throw new HttpExp(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw e;
            }
        }

        public async Task<TR> PostAsync<T, TR>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                Debug.WriteLine($"{"PostAsync Data : " + JsonConvert.SerializeObject(data)}");


                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceExp(jsonResult, responseMessage.StatusCode);
                }

                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ApplicationException(jsonResult.ToString().Replace("\"", ""));
                }

                throw new HttpExp(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                Debug.WriteLine($"{"PutAsync Data : " + JsonConvert.SerializeObject(data)}");

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceExp(jsonResult, responseMessage.StatusCode);
                }


                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ApplicationException(jsonResult.ToString().Replace("\"", ""));
                }


                throw new HttpExp(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<TR> PutAsync<T, TR>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                Debug.WriteLine($"{"PutAsync Data : " + JsonConvert.SerializeObject(data)}");

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));


                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {

                    throw new ServiceExp(jsonResult, responseMessage.StatusCode);
                }

                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ApplicationException(jsonResult.ToString().Replace("\"", ""));
                }


                throw new HttpExp(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            await httpClient.DeleteAsync(uri);
        }

        private HttpClient CreateHttpClient(string authToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            return httpClient;
        }

        private async Task<string> GetCustomRequestHeader()
        {
            //// eg : 14.651457|121.049228
            //var coor = await _zoneService.GetAssignedLatitudeLongitudeAsync();
            //var coordinate = coor.Latitude + "|" + coor.Longitude;

            //var deviceID = _phoneService.DeviceInformation.Id;

            //return string.Format("{0}|{1}|", coordinate, deviceID);

            return await Task.FromResult(string.Empty);
        }

        public async Task<string> UploadImageAsync(Stream image, string fileName, string uri, string authToken)
        {
            string received;

            try
            {
                HttpContent fileStreamContent = new StreamContent(image);

                fileStreamContent.Headers.ContentDisposition =

                new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                {
                    Name = "uploadedFile"
                    ,
                    FileName = fileName
                };

                fileStreamContent.Headers.ContentType = new

                System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                using (var client = CreateHttpClient(authToken))
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        formData.Add(fileStreamContent);

                        var response = await client.PostAsync(uri, formData);

                        if (response.IsSuccessStatusCode)
                        {
                            received = await response.Content.ReadAsStringAsync();
                            received = received.Replace("\"", "");
                        }
                        else
                        {
                            received = null;

                            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            if (response.StatusCode == HttpStatusCode.BadRequest)
                            {
                                throw new ApplicationException(jsonResult.ToString().Replace("\"", ""));
                            }

                            throw new HttpExp(response.StatusCode, response.ToString());
                        }
                    }
                }

                Debug.WriteLine(received);

                return received;

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");

                throw;
            }
        }


    }


}
