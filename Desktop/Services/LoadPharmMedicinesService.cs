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
        private PharmaciesViewModel pharmaciesViewModel;

        private readonly InstitutionClient.Client instClient;

        private readonly Client medClient;

        public int CurrentPharamcy;

        public LoadPharmMedicinesService(PharmaciesViewModel pharmaciesViewModel)
        {
            this.pharmaciesViewModel = pharmaciesViewModel;
            this.instClient = ((App)App.Current).InstitutionClient;
            this.medClient = ((App)App.Current).MedicineClient;
        }

        public async Task Load()
        {
            var response = await this.instClient.GetPharmacyMedicinesAsync(this.CurrentPharamcy);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            this.pharmaciesViewModel.Medicines = await this.GetMedicinePricePair(response.Content);
        }

        private async Task<ObservableCollection<MedicinePricePair>> GetMedicinePricePair(IEnumerable<PharmMedicine> medicines)
        {
            var result = new ObservableCollection<MedicinePricePair>();

            foreach (var medicine in medicines)
            {
                var response = await this.medClient.GetMedicineAsync($"api/medicines/{medicine.MedicineId}");

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
