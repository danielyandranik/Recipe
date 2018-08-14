using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using MedicineApiClient;
using Desktop.ViewModels;
using Desktop.Views.Windows;

namespace Desktop.Services
{
    /// <summary>
    /// Class for medicines load
    /// </summary>
    class LoadMedicinesService
    {
        /// <summary>
        /// Medicines page view model
        /// </summary>
        private readonly LoadablePageViewModel viewModel;

        /// <summary>
        /// Medicine API client
        /// </summary>
        private readonly Client client;

        /// <summary>
        /// Creates new instance of <see cref="LoadMedicinesService"/>
        /// </summary>
        /// <param name="viewModel">Medidines page viewmodel</param>
        public LoadMedicinesService(LoadablePageViewModel viewModel)
        {
            // setting vm
            this.viewModel = viewModel;
            
            // setting Medicine API client
            this.client = ((App)App.Current).MedicineClient;
        }

        /// <summary>
        /// Loads medicines
        /// </summary>
        /// <returns>nothing</returns>
        public async Task Load()
        {
            // setting visibilities
            this.viewModel.SetVisibilities(Visibility.Visible, true);

            // getting messgae
            var msg = (string)App.Current.Resources["med_load_error"];

            // loading
            try
            {
                var response = await this.client.GetAllMedicinesAsync("api/medicines");

                if (!response.IsSuccessStatusCode)
                {
                    RecipeMessageBox.Show(msg);
                }

                if (this.viewModel is MedicinesViewModel)
                {
                    ((MedicinesViewModel)this.viewModel).Medicines = new ObservableCollection<Medicine>(response.Result);
                    ((MedicinesViewModel)this.viewModel).Data = ((MedicinesViewModel)this.viewModel).Medicines;
                }
                else if (this.viewModel is CreateRecipeViewModel)
                {
                    ((CreateRecipeViewModel)this.viewModel).Medicines = new ObservableCollection<Medicine>(response.Result);
                }
            }
            catch
            {
                RecipeMessageBox.Show(msg);
            }
            finally
            {
                this.viewModel.SetVisibilities(Visibility.Collapsed, false);
            }
        }
    }
}
