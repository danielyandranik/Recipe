using GalaSoft.MvvmLight;
using MedicineApiClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class MedicinesViewModel : ViewModelBase
    {
        private ObservableCollection<Medicine> _medicines;

        public ObservableCollection<Medicine> Medicines
        {
            get
            {
                return this._medicines;
            }
            set
            {
                this.Set("Medicines", ref this._medicines, value);
            }
        }

        public MedicinesViewModel()
        {
            this.LoadMedicines();
        }

        private void LoadMedicines()
        { 
            var client = new Client(ConfigurationSettings.AppSettings["MedicineAPI"]);
            var response = client.GetAllMedicinesAsync("api/medicines").Result;
            if(!response.IsSuccessStatusCode)
            {
                Debug.Write(response.StatusCode);
                return;
            }

            this.Medicines = new ObservableCollection<Medicine>(response.Result);
        }

    }
}
