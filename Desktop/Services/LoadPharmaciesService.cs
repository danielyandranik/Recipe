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
            var response = await this.client.GetAllPharmaciesAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            this.pharmaciesViewModel.Pharmacies = new ObservableCollection<Institution>(response.Content);
        }
    }
}
