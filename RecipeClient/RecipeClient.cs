using Newtonsoft.Json;
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

        public async Task<ResponseMessage<IEnumerable<Recipe>>> GetAllRecipesAsync()
        {
            var httpResponse = await this.client.GetAsync(String.Empty);

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<IEnumerable<Recipe>>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Result = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(content)
            };
        }

        public async Task<ResponseMessage<Recipe>> GetRecipeAsync(string requestUri)
        {
            var httpResponse = await this.client.GetAsync(requestUri);

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<Recipe>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Result = JsonConvert.DeserializeObject<Recipe>(content)
            };
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            var json = JsonConvert.SerializeObject(recipe);

            var httpResponse = await this.client.PostAsync(String.Empty, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task<bool> UpdateRecipeAsync(Recipe recipe)
        {
            var json = JsonConvert.SerializeObject(recipe);

            var httpResponse = await this.client.PutAsync("?id=" + recipe.Id, new StringContent(json, Encoding.UTF8, "application/json"));

            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRecipeeAsync(string requestUri)
        {

            var httpResponse = await this.client.DeleteAsync(requestUri);

            return httpResponse.IsSuccessStatusCode;
        }
    }
}
