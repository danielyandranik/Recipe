using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Desktop.ViewModels;
using MedicineApiClient;

namespace Desktop.Services
{
    class LoadMedicinesService
    {
        private MedicinesViewModel medicinesViewModel;

        private readonly Client client;

        public LoadMedicinesService(MedicinesViewModel medicinesViewModel)
        {
            this.medicinesViewModel = medicinesViewModel;
            this.client = ((App)App.Current).MedicineClient;
        }

        public async Task Load()
        {
            var response = await this.client.GetAllMedicinesAsync("api/medicines");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            this.medicinesViewModel.Medicines =  new ObservableCollection<Medicine>(response.Result);
        }
    }
}
