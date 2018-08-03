using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Desktop.ViewModels;
using InstitutionClient;
using InstitutionClient.Models;

namespace Desktop.Services
{
    public class LoadPharmaciesService
    {
        private PharmaciesViewModel pharmaciesViewModel;

        private readonly Client client;

        public LoadPharmaciesService(PharmaciesViewModel pharmaciesViewModel)
        {
            this.pharmaciesViewModel = pharmaciesViewModel;
            this.client = ((App)App.Current).InstitutionClient;
        }

        public async Task Load()
        {
           // var medResponse = await this.client.GetAllPharmacyMedicinesAsync();
            var pharmResponse = await this.client.GetAllPharmaciesAsync();

            if (!pharmResponse.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            this.pharmaciesViewModel.PharmMedicines = new ObservableCollection<PharmMedicine>();
            this.pharmaciesViewModel.Pharmacies = new ObservableCollection<Institution>(pharmResponse.Content);
            this.pharmaciesViewModel.data = this.pharmaciesViewModel.Pharmacies;
        }
    }
}
