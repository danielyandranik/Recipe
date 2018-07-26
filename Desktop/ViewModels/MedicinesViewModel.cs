using Desktop.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Medicines ViewModel
    /// </summary>
    class MedicinesViewModel : ViewModelBase
    {
        private IEnumerable<Medicine> medicines;
    
        public IEnumerable<Medicine> Medicines
        {
            get => this.medicines;
            set => Set("Medicines", ref this.medicines, value);
        }
    }
}
