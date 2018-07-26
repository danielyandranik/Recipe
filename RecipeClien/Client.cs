using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClient
{
    public class Client
    {
        private HttpClient client;

        public Client()
        {
            this.client = new HttpClient();
            //this.client.DefaultRequestHeaders.
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

        public Task<HttpResponseMessage> CreateRecipeAsync(Recipe recipe)
        {
            var json = JsonConvert.SerializeObject(recipe);
            return this.client.PostAsync(this.BaseAddress, new StringContent(json));
        }
    }
}
