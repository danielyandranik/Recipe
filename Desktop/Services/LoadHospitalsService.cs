using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Desktop.ViewModels;
using Desktop.Interfaces;
using InstitutionClient;
using InstitutionClient.Models;

namespace Desktop.Services
{
    public class LoadHospitalsService : ILoadService
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
            this.hospitalsViewModel.data = this.hospitalsViewModel.Hospitals; 
        }
    }
}
