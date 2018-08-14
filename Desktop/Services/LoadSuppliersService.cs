using Desktop.Interfaces;
using Desktop.ViewModels;
using MedicineApiClient;
using InstitutionClient.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Desktop.Views.Windows;

namespace Desktop.Services
{
    public class LoadSuppliersService : ILoadService
    {
        private PharmaciesViewModel pharmaciesViewModel;

        private readonly InstitutionClient.Client instClient;

        private readonly Client medClient;

        public string MedicineName;

        public LoadSuppliersService(PharmaciesViewModel pharmaciesViewModel)
        {
            this.pharmaciesViewModel = pharmaciesViewModel;
            this.instClient = ((App)App.Current).InstitutionClient;
            this.medClient = ((App)App.Current).MedicineClient;
        }

        public async Task Load()
        {
            var medicineId = await this.GetMedicineId();
            
            var response = await this.instClient.GetAllSuppliersAsync(medicineId);

            if (!response.IsSuccessStatusCode)
            {
                RecipeMessageBox.Show("Couldn't find medicine suppliers.");
                return;
            }

            this.pharmaciesViewModel.Pharmacies = new ObservableCollection<Institution>(response.Content);
        }


        private async Task<int> GetMedicineId()
        {
            var response = await this.medClient.GetMedicineAsync($"api/medicines/?medicineName={this.MedicineName}");

            if (!response.IsSuccessStatusCode)
            {
                RecipeMessageBox.Show("Couldn't find the medicine.");
                return -1;
            }

            int.TryParse(response.Result.Id, out int id);

            return id;
        }
    }
}
