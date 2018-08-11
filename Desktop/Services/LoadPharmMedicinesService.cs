using Desktop.ViewModels;
using MedicineApiClient;
using InstitutionClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Models;
using Desktop.Interfaces;

namespace Desktop.Services
{
    public class LoadPharmMedicinesService : ILoadService
    {

        public int id;

        private PharmaciesViewModel pharmaciesViewModel;

        private readonly InstitutionClient.Client inst_client;

        private readonly Client med_client;

        public LoadPharmMedicinesService(PharmaciesViewModel pharmaciesViewModel)
        {
            this.pharmaciesViewModel = pharmaciesViewModel;
            this.inst_client = ((App)App.Current).InstitutionClient;
            this.med_client = ((App)App.Current).MedicineClient;
        }

        public async Task Load()
        {
            var response = await this.inst_client.GetPharmacyMedicinesAsync(this.id);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            // this.pharmaciesViewModel.Medicines = this.GetMedicinePricePair(response.Content).Result;
            this.pharmaciesViewModel.Medicines = new ObservableCollection<MedicinePricePair>();
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aaa", Price = 1000 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "aas", Price = 1500 });
            this.pharmaciesViewModel.Medicines.Add(new MedicinePricePair { Name = "f", Price = 1500 });
        }

        private async Task<ObservableCollection<MedicinePricePair>> GetMedicinePricePair(IEnumerable<PharmMedicine> medicines)
        {
            var result = new ObservableCollection<MedicinePricePair>();

            foreach (var medicine in medicines)
            {
                var response = await this.med_client.GetMedicineAsync($"api/medicines/{medicine.MedicineId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var medicinePrice = new MedicinePricePair
                {
                    Name = response.Result.Name,
                    Price = medicine.Price
                };

                result.Add(medicinePrice);
            }

            return result;
        }
    }
}
