using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Desktop.ViewModels;
using Desktop.Views.Windows;
using System.Windows;
using MedicineApiClient;

namespace Desktop.Services
{
    class LoadMedicinesService
    {
        private readonly LoadablePageViewModel viewModel;

        private readonly Client client;

        public LoadMedicinesService(LoadablePageViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.client = ((App)App.Current).MedicineClient;
        }

        public async Task Load()
        {
            this.viewModel.SetVisibilities(Visibility.Visible, true);

            var msg = (string)App.Current.Resources["med_load_error"];

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
                    ((MedicinesViewModel)this.viewModel).data = ((MedicinesViewModel)this.viewModel).Medicines;
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
