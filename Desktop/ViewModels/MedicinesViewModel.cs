using Desktop.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Medicines ViewModel
    /// </summary>
    class MedicinesViewModel
    {  
        public ObservableCollection<Medicine> Medicines { get; set; }

        private void LoadMedicines()
        {
            var medicines = new ObservableCollection<Medicine>();
            var medicineClient = new MedicineApiClient.Client();
        }
    }
}
