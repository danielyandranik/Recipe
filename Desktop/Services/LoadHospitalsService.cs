using Desktop.ViewModels;
using InstitutionClient;
using InstitutionClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Desktop.Services
{
    public class LoadHospitalsService
    {
        private HospitalsViewModel hospitalsViewModel;

        private readonly Client client;

        public LoadHospitalsService(HospitalsViewModel hospitalsViewModel)
        {
            this.hospitalsViewModel = hospitalsViewModel;
            this.client = ((App)App.Current).InstitutionClient;
        }

        public async Task Load()
        {
            var response = await this.client.GetAllHospitalsAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            this.hospitalsViewModel.Hospitals = new ObservableCollection<Institution>(response.Content);
        }
    }
}
