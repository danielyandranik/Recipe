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
    /// <summary>
    /// Service for loading pharmacies
    /// </summary>
    public class LoadPharmaciesService : ILoadService
    {
        /// <summary>
        /// Pharmacies page view model
        /// </summary>
        private PharmaciesViewModel pharmaciesViewModel;

        /// <summary>
        /// Institution client
        /// </summary>
        private readonly Client client;

        /// <summary>
        /// Creates new instance of <see cref="LoadPharmaciesService"/>
        /// </summary>
        /// <param name="pharmaciesViewModel"></param>
        public LoadPharmaciesService(PharmaciesViewModel pharmaciesViewModel)
        {
            // setting vm
            this.pharmaciesViewModel = pharmaciesViewModel;

            // getting Institution API client
            this.client = ((App)App.Current).InstitutionClient;
        }

        /// <summary>
        /// Loads content
        /// </summary>
        /// <returns>nothing</returns>
        public async Task Load()
        {
            // sets visibilities
            this.pharmaciesViewModel.SetVisibilities(Visibility.Visible, true);

            // getting message
            var msg = (string)App.Current.Resources["pharmacies_load_error"];

            // loading
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
