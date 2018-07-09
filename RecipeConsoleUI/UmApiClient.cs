using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RecipeConsoleUI
{
    /// <summary>
    /// Client for consuming User Management API
    /// </summary>
    public class UmApiClient
    {
        /// <summary>
        /// Access token
        /// </summary>
        private string _accessToken;

        /// <summary>
        /// Http client
        /// </summary>
        private HttpClient httpClient;

        /// <summary>
        /// Creates new instance of <see cref="UmApiClient"/>
        /// </summary>
        public UmApiClient()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("http://localhost:5800");
            this.httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Registers user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>HTTP response message</returns>
        public HttpResponseMessage RegisterUser(UserRegisterInfo user)
        { 
            var json = JsonConvert.SerializeObject(user);

            var response = this.httpClient.PostAsync("api/users", 
                new StringContent(json,Encoding.UTF8,"application/json")).Result;

            return response;
        }

        public void LogIn(string username,string password)
        {
            var disco = DiscoveryClient.GetAsync("http://localhost:5700").Result;

            var tokenClient = new TokenClient(
                disco.TokenEndpoint,
                "RecipeConsoleUI",
                "secret");

            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync(
                username, password, "UserManagementAPI").Result;

            this._accessToken = tokenResponse.AccessToken;

            this.httpClient.SetBearerToken(this._accessToken);
        }

        public IEnumerable<UserPublicInfo> GetUserPublicInfos()
        {
            var response = this.httpClient.GetAsync("api/users").Result;

            var content = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<IEnumerable<UserPublicInfo>>(content);
        }

        public HttpResponseMessage AddDoctorProfile(Doctor doctor)
        {
            var response = this.httpClient.PostAsync("api/doctors", 
                new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json")).Result;
            return response;
        }
    }
}
