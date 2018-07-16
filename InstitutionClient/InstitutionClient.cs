using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InstitutionClient
{
    public class InstitutionClient
    {
        private readonly HttpClient client;

        public InstitutionClient(string baseAddress)
        {
            this.client = new HttpClient() { BaseAddress = new Uri(baseAddress) };
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public AuthenticationHeaderValue Authurization
        {
            get
            {
                return this.client.DefaultRequestHeaders.Authorization;
            }
            set
            {
                this.client.DefaultRequestHeaders.Authorization = value;
            }
        }

        public async Task<ResponseMessage<IEnumerable<T>>> GetAllAsync<T>()
        {
            var httpResponse = await this.client.GetAsync(String.Empty);

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<IEnumerable<T>>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Result = JsonConvert.DeserializeObject<IEnumerable<T>>(content)
            };
        }

        public async Task<ResponseMessage<T>> GetAsync<T>(string requestUri)
        {
            var httpResponse = await this.client.GetAsync(requestUri);

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<T>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Result = JsonConvert.DeserializeObject<T>(content)
            };
        }

        public async Task CreateAsync<T>(T item)
        {
            var json = JsonConvert.SerializeObject(item);

            var httpResponse = await this.client.PostAsync(String.Empty, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task<bool> UpdateAsync<T>(object id, T item)
        {
            var json = JsonConvert.SerializeObject(item);

            var httpResponse = await this.client.PutAsync("?id=" + id, new StringContent(json, Encoding.UTF8, "application/json"));

            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync<T>(string requestUri)
        {

            var httpResponse = await this.client.DeleteAsync(requestUri);

            return httpResponse.IsSuccessStatusCode;
        }
    }
}