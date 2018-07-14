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

		public async Task<ResponseMessage<IEnumerable<Medicine>>> GetAllMedicines()
		{
			return await new Task<ResponseMessage<IEnumerable<Medicine>>>(() =>
			 {
				 var httpResponse = this.client.GetAsync("").Result;

				 return new ResponseMessage<IEnumerable<Medicine>>
				 {
					 StatusCode = (int)httpResponse.StatusCode,
					 Result = JsonConvert.DeserializeObject<IEnumerable<Medicine>>(httpResponse.Content.ReadAsStringAsync().Result)
				 };
			 });				
		}

		public async Task<ResponseMessage<Medicine>> GetMedicine(string id)
		{
			return await new Task<ResponseMessage<Medicine>>(() =>
			{
				var httpResponse = this.client.GetAsync("").Result;

				return new ResponseMessage<Medicine>
				{
					StatusCode = (int)httpResponse.StatusCode,
					Result = JsonConvert.DeserializeObject<Medicine>(httpResponse.Content.ReadAsStringAsync().Result)
				};
			});
		}

		public Task CreateMedicine()
		{

		}

		public Task<IEnumerable<Medicine>> UpdateMedicine()
		{

		}

		public Task<IEnumerable<Medicine>> DeleteMedicine()
		{

		}
	}
}
