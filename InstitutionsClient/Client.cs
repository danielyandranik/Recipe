using AuthTokenService;
using InstitutionClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InstitutionClient
{
    public class Client : IDisposable
    {
        /// <summary>
        /// Http client for consuming Institutions API
        /// </summary>
        private readonly HttpClient client;

        /// <summary>
        /// Access token
        /// </summary>
        private string accessToken;

        /// <summary>
        /// Creates new instance of <see cref="Client"/>
        /// </summary>
        /// <param name="baseAddress">Institutions API address</param>
        public Client(string baseAddress)
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

        /// <summary>
        /// Event handler for TokenProvider TokenUpdated class
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        public void UpdateToken(object sender, TokenEventArgs e)
        {
            this.accessToken = e.AccessToken;
            this.client.SetBearerToken(this.accessToken);
        }

        /// <summary>
        /// Disposes Institutions API client
        /// </summary>
        public void Dispose()
        {
            this.client.Dispose();
        }

        private async Task<ResponseMessage<IEnumerable<T>>> GetAllAsync<T>(string requestUri)
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

        private async Task<ResponseMessage<T>> GetAsync<T>(string requestUri)
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

        private async Task<ResponseMessage<string>> CreateAsync<T>(string requestUri, T t)
        {
            var json = JsonConvert.SerializeObject(t);

            var httpResponse = await this.client.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));

            var content = await httpResponse.Content.ReadAsStringAsync();

            return new ResponseMessage<string>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                Content = content
            };
        }

        private async Task<ResponseMessage<string>> UpdateAsync<T>(string requesUri, T t)
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

        private async Task<ResponseMessage<string>> DeleteAsync(string requestUri)
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

        public async Task<ResponseMessage<IEnumerable<Institution>>> GetAllHospitalsAsync()
        {
            return await this.GetAllAsync<Institution>("api/institutions/?type=hospital");
        }

        public async Task<ResponseMessage<IEnumerable<Institution>>> GetHospitalsByNameAsync(string name)
        {
            return await this.GetAllAsync<Institution>($"api/institutions/?type=hospital&name={name}");
        }

        public async Task<ResponseMessage<IEnumerable<Institution>>> GetHospitalsByAddressAsync(string address)
        {
            return await this.GetAllAsync<Institution>($"api/institutions/?type=hospital&name={address}");
        }

        public async Task<ResponseMessage<IEnumerable<Institution>>> GetAllPharmaciesAsync()
        {
            return await this.GetAllAsync<Institution>("api/institutions/?type=pharmacy");
        }

        public async Task<ResponseMessage<IEnumerable<Institution>>> GetPharmaciesByNameAsync(string name)
        {
            return await this.GetAllAsync<Institution>($"api/institutions/?type=pharmacy&name={name}");
        }

        public async Task<ResponseMessage<IEnumerable<Institution>>> GetPharmaciesByAddressAsync(string address)
        {
            return await this.GetAllAsync<Institution>($"api/institutions/?type=pharmacy&address={address}");
        }

        public async Task<ResponseMessage<IEnumerable<Institution>>> GetAllSuppliersAsync(int medicineId)
        {
            return await this.GetAllAsync<Institution>($"api/institutions/?medicineId={medicineId}");
        }

        public async Task<ResponseMessage<IEnumerable<PharmMedicine>>> GetAllPharmacyMedicinesAsync(int pharmacyId)
        {
            return await this.GetAllAsync<PharmMedicine>($"api/pharmmeds/?pharmacyId={pharmacyId}");
        }

        public async Task<ResponseMessage<Institution>> GetInstitution(int id)
        {
            return await this.GetAsync<Institution>($"api/institutions/?id={id}");
        }

        public async Task<ResponseMessage<PharmMedicine>> GetPharmacyMedicine(int id)
        {
            return await this.GetAsync<PharmMedicine>($"api/pharmmeds/?id={id}");
        }

        public async Task<bool> CreateInstitution(Institution institution)
        {
            var response = await this.CreateAsync<Institution>("api/institutions", institution);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreatePharmacyMedicine(PharmMedicine medicine)
        {
            var response = await this.CreateAsync<PharmMedicine>("api/pharmmeds", medicine);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateInstitutionAsync(Institution institution)
        {
            var response = await this.UpdateAsync<Institution>("api/institutions", institution);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMedicinePriceAsync(MedicinePriceInfo price)
        {
            var response = await this.UpdateAsync<MedicinePriceInfo>("api/pharmmeds/price", price);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMedicineQuantityAsync(MedicineQuantityInfo quantity)
        {
            var response =  await this.UpdateAsync<MedicineQuantityInfo>("api/pharmmeds/quantity", quantity);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePharmacyMedicineAsync(int id)
        {
            var response = await this.DeleteAsync($"api/pharmmeds/{id}");

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteInstitutionAsync(int id)
        {
            var response =  await this.DeleteAsync($"api/institutions/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
