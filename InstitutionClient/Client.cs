using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace InstitutionClient
{
    public class Client
    {
        /// <summary>
        /// Base address URI
        /// </summary>
        private Uri uri;

        /// <summary>
        /// Http client for making HTTP requests
        /// </summary>
        private HttpClient httpClient;

        /// <summary>
        /// Creates new instance of RestClient
        /// </summary>
        /// <param name="uri">Base address URI</param>
        public Client(string uri)
        {
            // if uri is null then throw an exception informing about that
            if (uri == null)
            {
                throw new NoNullAllowedException("Uri");
            }

            // constructing URI
            this.uri = new Uri(uri);

            // constructing http client
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = this.uri;
            this.httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue($"application/json"));
        }

        /// <summary>
        /// Sends HTTP GET request
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="resourcePath">Resource path</param>
        /// <returns>Returns response result.</returns>
        public ResponseResult Get<T>(string resourcePath)
        {
            // constructing response result
            var responseResult = new ResponseResult();

            // sending GET request
            var response = this.httpClient.GetAsync(resourcePath).Result;

            // if request is not successful
            if (!response.IsSuccessStatusCode)
            {
                // construct response result about failure
                responseResult.Status = Status.Failure;
                responseResult.Content = null;

                // return response result
                return responseResult;
            }

            // reading response
            var result = response.Content.ReadAsStringAsync().Result;

            // deserializing response
            var list = JsonConvert.DeserializeObject<T>(result);

            // constructing response result about success
            responseResult.Status = Status.Success;
            responseResult.Content = list;

            // return response result
            return responseResult;
        }

        /// <summary>
        /// Sends HTTP POST request
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="item">Item</param>
        /// <param name="resourcePath">Resource path</param>
        /// <returns>Returns response result</returns>
        public ResponseResult Post<T>(T item, string resourcePath)
        {
            // constructing response result
            var responseResult = new ResponseResult();

            // serializing item
            var serializedItem = JsonConvert.SerializeObject(item);

            // constructing String Content
            var httpContent = new StringContent(
                serializedItem, Encoding.UTF8, $"application/json");

            // sending POST request
            var response = this.httpClient.PostAsync(resourcePath, httpContent).Result;

            // if request is not successful
            if (!response.IsSuccessStatusCode)
            {
                // construct response result about failure 
                responseResult.Status = Status.Failure;
                responseResult.Content = null;

                // return response result
                return responseResult;
            }

            // otherwise construct response result about success
            responseResult.Status = Status.Success;
            responseResult.Content = "Successfully posted";

            // return response result
            return responseResult;
        }

        /// <summary>
        /// Sends HTTP PUT request
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="id">ID</param>
        /// <param name="item">Item</param>
        /// <param name="resourcePath">Resource path</param>
        /// <returns>Returns response result</returns>
        public ResponseResult Put<T>(int id, T item, string resourcePath)
        {
            // constructing response resuly
            var responseResult = new ResponseResult();

            var data = new DataToPost<T>() { Id = id, Item = item };

            // serializing item
            var serializedItem = JsonConvert.SerializeObject(data);

            // constructing String Content
            var httpContent = new StringContent(
                serializedItem, Encoding.UTF8, $"application/json");

            // sending PUT request
            var response = this.httpClient.PutAsync(resourcePath, httpContent).Result;

            // if request is not successful
            if (!response.IsSuccessStatusCode)
            {
                // construct response result about failure
                responseResult.Status = Status.Failure;
                responseResult.Content = null;

                // return response result
                return responseResult;
            }

            // otherwise construct response result about success
            responseResult.Status = Status.Success;
            responseResult.Content = "Successfully put";

            // return response result
            return responseResult;
        }

        /// <summary>
        /// Sends DELETE request
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="resourcePath">Resource path</param>
        /// <returns>Returns response result</returns>
        public ResponseResult Delete(int id, string resourcePath)
        {
            // constructing response result
            var responseResult = new ResponseResult();

            // sending DELETE request
            var response = this.httpClient.DeleteAsync($"{resourcePath}/{id}").Result;

            // if request is not successful
            if (!response.IsSuccessStatusCode)
            {
                // construct response result about failure
                responseResult.Status = Status.Failure;
                responseResult.Content = null;

                // return response result
                return responseResult;
            }

            // otherwise construct response result about success
            responseResult.Status = Status.Success;
            responseResult.Content = "Successfully deleted.";

            // return response result
            return responseResult;
        }
    }
}


