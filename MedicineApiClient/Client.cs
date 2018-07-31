using Newtonsoft.Json;
using AuthTokenService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MedicineApiClient
{
    public class Client
    {
		private readonly HttpClient client;

        private string _accessToken;

		public Client(string baseAddress)
		{
			this.client = new HttpClient() { BaseAddress = new Uri(baseAddress) };
			this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.SetBearerToken(this._accessToken);
        }

        /// <summary>
        /// Event handler for TokenProvider TokenUpdated class
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        public void UpdateToken(object sender, TokenEventArgs e)
        {
            this._accessToken = e.AccessToken;
            this.client.SetBearerToken(this._accessToken);
        }

        public async Task<ResponseMessage<IEnumerable<Medicine>>> GetAllMedicinesAsync(string requestUri)
		{
            var httpResponse = await this.client.GetAsync(requestUri);

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<IEnumerable<Medicine>>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Result = JsonConvert.DeserializeObject<IEnumerable<Medicine>>(content)
            };			
		}

        public async Task<ResponseMessage<Medicine>> GetMedicineAsync(string requestUri)
		{
            var httpResponse = await this.client.GetAsync(requestUri);

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<Medicine>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Result = JsonConvert.DeserializeObject<Medicine>(content)
            };
        }

		public async Task<bool> CreateMedicineAsync(Medicine medicine)
		{
            var json = JsonConvert.SerializeObject(medicine);

            var httpResponse = await this.client.PostAsync("api/medicines", new StringContent(json, Encoding.UTF8, "application/json"));

            return httpResponse.IsSuccessStatusCode;
            //var content = await httpResponse.Content.ReadAsStringAsync();
        }

		public async Task<bool> UpdateMedicineAsync(Medicine medicine)
		{
            var json = JsonConvert.SerializeObject(medicine);

            var httpResponse = await this.client.PutAsync($"api/medicines/{medicine.Id}", new StringContent(json, Encoding.UTF8, "application/json"));

            //var content = await httpResponse.Content.ReadAsStringAsync();

            return httpResponse.IsSuccessStatusCode;
        }

		public async Task<bool> DeleteMedicineAsync(string requestUri)
		{

            var httpResponse = await this.client.DeleteAsync(requestUri);

            //var content = await httpResponse.Content.ReadAsStringAsync();

            return httpResponse.IsSuccessStatusCode;
        }
	}
}
