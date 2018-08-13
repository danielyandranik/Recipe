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
            this.hospitalsViewModel.SetVisibilities(Visibility.Visible, true);

            var msg = (string)App.Current.Resources["hospitals_load_error"];

            try
            {
                var response = await this.client.GetAllHospitalsAsync();

                if (!response.IsSuccessStatusCode)
                {
                    RecipeMessageBox.Show(msg);
                }

                this.hospitalsViewModel.Hospitals = new ObservableCollection<Institution>(response.Content);
                this.hospitalsViewModel.data = this.hospitalsViewModel.Hospitals;
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
