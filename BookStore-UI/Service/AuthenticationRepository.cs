using Blazored.LocalStorage;
using BookStore_UI.Contracts;
using BookStore_UI.Models;
using BookStore_UI.Providers;
using BookStore_UI.Static;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace BookStore_UI.Service
{
    public class AuthenticationRepository : IAuthenticationRepository

    {

        private readonly IHttpClientFactory _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _stateProvider;
        

        public AuthenticationRepository(IHttpClientFactory client, ILocalStorageService localStorage,
            AuthenticationStateProvider stateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _stateProvider = stateProvider;
        }
        public async Task<bool> Register(RegistrationModel user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, EndPoints.RegistersEndPoint);

            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var clientu = _client.CreateClient();
            HttpResponseMessage response = await clientu.SendAsync(request);
            return response.IsSuccessStatusCode;
            
        }

        public async Task<bool> Login(LoginModel user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, EndPoints.LoginEndPoint);

            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var clientu = _client.CreateClient();
            HttpResponseMessage response = await clientu.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            var token =  JsonConvert.DeserializeObject<TokenResponse>(content);

            //store token
            await _localStorage.SetItemAsync("authToken", token.Token);

            // auth state for app

            await ((ApiAuthenticationStateProvider)_stateProvider).LoggedIn();

            clientu.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);
            
            return true;

        }

        public async Task  Logout()
        {
           await _localStorage.RemoveItemAsync("authToken");
          ((ApiAuthenticationStateProvider)_stateProvider).LoggedOut();
        }
    }
}
