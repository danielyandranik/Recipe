using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MedicineApiClient
{
    public class Client
    {
		private readonly HttpClient client;

		public Client(string baseAddress)
		{
			this.client = new HttpClient() { BaseAddress = new Uri(baseAddress) };
			this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<ResponseMessage<IEnumerable<Medicine>>> GetAllMedicinesAsync()
		{
            var httpResponse = await this.client.GetAsync(String.Empty);

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

		public async Task CreateMedicineAsync(Medicine medicine)
		{
            var json = JsonConvert.SerializeObject(medicine);

            var httpResponse = await this.client.PostAsync(String.Empty, new StringContent(json));

            //var content = await httpResponse.Content.ReadAsStringAsync();
        }

		public async Task<bool> UpdateMedicineAsync(Medicine medicine)
		{
            var json = JsonConvert.SerializeObject(medicine);

            var httpResponse = await this.client.PutAsync(medicine.Id, new StringContent(json));

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
