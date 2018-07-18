using MedicineAPI.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MedicineClient
{
    public class Client
    {
        private HttpClient client;

        public Client()
        {
            this.client = new HttpClient();

        }

        public Uri BaseAddress
        {
            get
            {
                return this.client.BaseAddress;
            }

            set
            {
                this.client.BaseAddress = value;
            }
        }

        public Task<HttpResponseMessage> CreateMedicineAsync(Medicine medicine)
        {
            var json = JsonConvert.SerializeObject(medicine);
            return this.client.PostAsync(this.BaseAddress, new StringContent(json));
        }
    }
}
