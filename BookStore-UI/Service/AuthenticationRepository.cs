using BookStore_UI.Contracts;
using BookStore_UI.Models;
using BookStore_UI.Static;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_UI.Service
{
    public class AuthenticationRepository : IAuthenticationRepository

    {

        private readonly IHttpClientFactory _client;

        public AuthenticationRepository(IHttpClientFactory client)
        {
            _client = client;
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
            var token = JsonConvert.DeserializeObject<TokenResponse>(content);

        }

        public async Task  Logout()
        {
            throw new NotImplementedException();
        }
    }
}
