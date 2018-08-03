using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Desktop.ViewModels;
using MedicineApiClient;
using GalaSoft.MvvmLight;

namespace Desktop.Services
{
    class LoadMedicinesService
    {
        private readonly ViewModelBase viewModel;

        private readonly Client client;

        public LoadMedicinesService(ViewModelBase viewModel)
        {
            this.viewModel = viewModel;
            this.client = ((App)App.Current).MedicineClient;
        }

        public async Task Load()
        {
            var response = await this.client.GetAllMedicinesAsync("api/medicines");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            if(this.viewModel is MedicinesViewModel)
            {
                ((MedicinesViewModel)this.viewModel).Medicines = new ObservableCollection<Medicine>(response.Result);
            }
            else if(this.viewModel is CreateRecipeViewModel)
            {
                ((CreateRecipeViewModel)this.viewModel).Medicines = new ObservableCollection<Medicine>(response.Result);
            }
        }
    }
}
