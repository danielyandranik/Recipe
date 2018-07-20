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
    public class Client
    {
        private readonly HttpClient client;

        public Client(string baseAddress)
        {
            // api.institutions? base address??
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
            return await this.GetAllAsync<Institution>("api/institutions/hospital");
        }

        public async Task<ResponseMessage<IEnumerable<Institution>>> GetAllPharmaciessAsync()
        {
            return await this.GetAllAsync<Institution>("api/institutions/pharmacy");
        }

        public async Task<ResponseMessage<IEnumerable<Institution>>> GetAllSuppliersAsync(int medicineId)
        {
            return await this.GetAllAsync<Institution>($"api/institutions/pharmacy/?id={medicineId}");
        }

        public async Task<ResponseMessage<IEnumerable<PharmMedicine>>> GetAllPharmacyMedicinesAsync(int pharmacyId)
        {
            return await this.GetAllAsync<PharmMedicine>($"api/pharmmeds/{pharmacyId}");
        }

        public async Task<ResponseMessage<Institution>> GetInstitution(int id)
        {
            return await this.GetAsync<Institution>($"api/institutions/{id}");
        }

        public async Task<ResponseMessage<PharmMedicine>> GetPharmacyMedicine(int id)
        {
            return await this.GetAsync<PharmMedicine>($"api/pharmmeds/{id}");
        }

        public async Task<ResponseMessage<string>> CreateInstitution(Institution institution)
        {
            return await this.CreateAsync<Institution>("api/institutions",institution);
        }

        public async Task<ResponseMessage<string>> CreatePharmacyMedicine(PharmMedicine medicine)
        {
            return await this.CreateAsync<PharmMedicine>( "api/pharmmeds",medicine);
        }

        public async Task<ResponseMessage<string>> UpdateInstitutionAsync(Institution institution)
        {
            return await this.UpdateAsync<Institution>("api/institutions", institution);
        }

        public async Task<ResponseMessage<string>> UpdateMedicinePriceAsync(MedicinePriceInfo price)
        {
            return await this.UpdateAsync<MedicinePriceInfo>("api/pharmmeds/price", price);
        }

        public async Task<ResponseMessage<string>> UpdateMedicineQuantityAsync(MedicineQuantityInfo quantity)
        {
            return await this.UpdateAsync<MedicineQuantityInfo>("api/pharmmeds/quantity", quantity);
        }

        private async Task<ResponseMessage<string>> DeletePharmacyMedicineAsync(int id)
        {
            return await this.DeleteAsync($"api/pharmmeds/{id}");
        }
    }
}
