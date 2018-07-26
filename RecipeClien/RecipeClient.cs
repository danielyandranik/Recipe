﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClient
{
    public class RecipeClient
    {
        private readonly HttpClient client;

        public RecipeClient(string baseAddress)
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

        public async Task<ResponseMessage<IEnumerable<T>>> GetAllAsync<T>(string requestUri)
        {
            var httpResponse = await this.client.GetAsync(requestUri);

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<IEnumerable<T>>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Content = JsonConvert.DeserializeObject<IEnumerable<T>>(content)
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
                Content = JsonConvert.DeserializeObject<T>(content)
            };
        }

        public async Task<ResponseMessage<string>> CreateAsync<T>(T t)
        {
            var json = JsonConvert.SerializeObject(t);

            var httpResponse = await this.client.PostAsync(String.Empty, new StringContent(json, Encoding.UTF8, "application/json"));

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<string>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Content = content
            };
        }

        public async Task<ResponseMessage<string>> UpdateAsync<T>(string requesUri, T t)
        {
            var json = JsonConvert.SerializeObject(t);

            var httpResponse = await this.client.PutAsync(requesUri, new StringContent(json, Encoding.UTF8, "application/json"));

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<string>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Content = content
            };
        }

        public async Task<ResponseMessage<string>> DeleteAsync(string requestUri)
        {

            var httpResponse = await this.client.DeleteAsync(requestUri);

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<string>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Content = content
            };
        }
    }
}