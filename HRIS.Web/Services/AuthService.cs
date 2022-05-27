using Blazored.LocalStorage;
using HRIS.Domain.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;

namespace HRIS.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;


        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage
                           , IConfiguration configuration)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _config = configuration;
        }

        //public async Task<RegisterResult> Register(RegisterModel registerModel)
        //{
        //    var result = await _httpClient.PostJsonAsync<RegisterResult>("api/accounts", registerModel);

        //    return result;
        //}

        public async Task<LoginResult> Login(LoginRequest loginRequest)
        {
            UriBuilder url = new UriBuilder("http://localhost:8722/")
            {
                Path = "api/Authorization/Login",
                Query = "username=" + loginRequest.Username + "&password=" + loginRequest.Password
            };

            var response = await _httpClient.PostAsJsonAsync<LoginResult>(url.ToString(), null);


            LoginResult loginResult = new LoginResult();

            if (response.IsSuccessStatusCode)
            {
                loginResult = JsonConvert.DeserializeObject<LoginResult>(await response.Content.ReadAsStringAsync());

                return loginResult;
            }

            var user = await GetUserDetails(loginRequest.Username);

            if (user.ContainsKey("Email"))
            {
                await _localStorage.SetItemAsync("authToken", loginResult.Token);

                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user["Email"].ToString());

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

                return loginResult;
            }

            throw new Exception("Invalid Login");
        }

        public async Task<Dictionary<string, string>> GetUserDetails(string username)
        {
            UriBuilder usrUrl = new UriBuilder("http://localhost:8722/")
            {
                Path = "api/Authorization/GetUserByUsername",
                Query = "username=" + username
            };

            var responseMessage = await _httpClient.GetFromJsonAsync<Dictionary<string, string>>(usrUrl.ToString());

            if (responseMessage.ContainsKey("Email"))
            {
                return responseMessage;
            }

            throw new Exception("No User Found");
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
