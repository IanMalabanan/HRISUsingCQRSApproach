using HRIS.Domain.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using HRIS.WPF.Interfaces;
using HRIS.WPF.Core;
using HRIS.WPF.Utilities;

namespace HRIS.WPF.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly HttpClient _httpClient;
       

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task<LoginResult> Login(LoginRequest loginRequest)
        {
            UriBuilder url = new UriBuilder(APIConstant.BaseUrl)
            {
                Path = "api/Authorization/Login",
                Query = "username=" + loginRequest.Username + "&password=" + loginRequest.Password
            };

            var response = await _httpClient.PostAsJsonAsync<LoginResult>(url.ToString(), null);

            LoginResult loginResult = new LoginResult();

            if (response.IsSuccessStatusCode)
            {
                loginResult = JsonConvert.DeserializeObject<LoginResult>(await response.Content.ReadAsStringAsync());

                APIConstant.AccessToken = loginResult.Token;

                var user = await GetUserDetails(loginRequest.Username);

                if (user.ContainsKey("userName"))
                {
                    return loginResult;
                }
            }

            throw new Exception("Invalid Login");
        }

        public async Task<Dictionary<string, string>> GetUserDetails(string username)
        {
            UriBuilder usrUrl = new UriBuilder(APIConstant.BaseUrl)
            {
                Path = "api/Authorization/GetUserByUsername",
                Query = "username=" + username
            };
            
            // Add an Accept header for JSON format.
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            HttpResponseMessage response = await _httpClient.GetAsync(usrUrl.ToString()).ConfigureAwait(false);  
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(products);

                if (!result.ContainsKey("email"))
                    throw new Exception("No User Found");

                return result;
                
            }
            else
            {
                throw new Exception("No User Found");
            }
            
        }

        public async Task Logout()
        {
            //await _localStorage.ClearAsync();
            //await _localStorage.RemoveItemAsync("authToken");
            //tokenProvider.AccessToken = string.Empty;
            //((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
