using Desktop.ViewModels;
using MedicineApiClient;
using InstitutionClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Models;

namespace Desktop.Services
{
    public class LoadPharmMedicinesService
    {
        private PharmMedicinesViewModel medicinesViewModel;

        private readonly InstitutionClient.Client inst_client;

        private readonly Client med_client;

        public LoadPharmMedicinesService(PharmMedicinesViewModel medicinesViewModel)
        {
            this.medicinesViewModel = medicinesViewModel;
            this.inst_client = ((App)App.Current).InstitutionClient;
            this.med_client = ((App)App.Current).MedicineClient;
        }

        public async Task Load()
        {
            var response = await this.inst_client.GetPharmacyMedicinesAsync(this.medicinesViewModel.pharmacyId);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            this.medicinesViewModel.PharmMedicines = new ObservableCollection<PharmMedicine>(response.Content);

            this.medicinesViewModel.MedicinePricePairs = this.GetMedicinePricePair(response.Content).Result;
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
