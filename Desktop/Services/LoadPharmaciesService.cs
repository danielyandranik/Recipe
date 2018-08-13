using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using InstitutionClient;
using InstitutionClient.Models;
using Desktop.ViewModels;
using Desktop.Interfaces;
using Desktop.Views.Windows;

namespace Desktop.Services
{
    public class LoadPharmaciesService : ILoadService
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
            this.pharmaciesViewModel.SetVisibilities(Visibility.Visible, true);

            var msg = (string)App.Current.Resources["pharmacies_load_error"];

            try
            {
                var response = await this.client.GetAllPharmaciesAsync();

                if (!response.IsSuccessStatusCode)
                {
                    RecipeMessageBox.Show(msg);
                }

                this.pharmaciesViewModel.Pharmacies = new ObservableCollection<Institution>(response.Content);
                this.pharmaciesViewModel.data = this.pharmaciesViewModel.Pharmacies;
            }
            catch
            {
                RecipeMessageBox.Show(msg);
            }
            finally
            {
                this.pharmaciesViewModel.SetVisibilities(Visibility.Collapsed, false);
            }
        }
    }
}
