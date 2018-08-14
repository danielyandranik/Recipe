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
    /// Hospital load service
    /// </summary>
    public class LoadHospitalsService : ILoadService
    {
        /// <summary>
        /// Hospitals view model
        /// </summary>
        private HospitalsViewModel hospitalsViewModel;

        /// <summary>
        /// Institution client
        /// </summary>
        private readonly Client client;

        /// <summary>
        /// Creates new instance of <see cref="LoadHospitalsService"/>
        /// </summary>
        /// <param name="hospitalsViewModel">Hospitals view model</param>
        public LoadHospitalsService(HospitalsViewModel hospitalsViewModel)
        {
            // setting vm
            this.hospitalsViewModel = hospitalsViewModel;

            // getting client
            this.client = ((App)App.Current).InstitutionClient;
        }

        /// <summary>
        /// Loads content
        /// </summary>
        /// <returns>nothing</returns>
        public async Task Load()
        {
            // setting visibilities
            this.hospitalsViewModel.SetVisibilities(Visibility.Visible, true);

            // getting message
            var msg = (string)App.Current.Resources["hospitals_load_error"];


            // loading
            try
            {
                var response = await this.client.GetAllHospitalsAsync();

                if (!response.IsSuccessStatusCode)
                {
                    RecipeMessageBox.Show(msg);
                }

                this.hospitalsViewModel.Hospitals = new ObservableCollection<Institution>(response.Content);
                this.hospitalsViewModel.Data = this.hospitalsViewModel.Hospitals;
            }
            catch
            {
                RecipeMessageBox.Show(msg);
            }
            finally
            {
                this.hospitalsViewModel.SetVisibilities(Visibility.Collapsed, false);
            }
        }
    }
}
