using Desktop.Commands;
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
using System.Windows;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public class MedicinesViewModel : ViewModelBase
    {
        private ObservableCollection<Medicine> medicines;

        private Medicine editableMedicine;

        private Visibility visibility;

        private DeleteMedicineCommand deleteMedicineCommand;

        private  EditMedicineCommand editMedicineCommand;

        public ObservableCollection<Medicine> Medicines
        {
            get
            {
                return this.medicines;
            }
            set
            {
                this.Set("Medicines", ref this.medicines, value);
            }
        }

        public Medicine EditableMedicine
        {
            get
            {
                return this.editableMedicine;
            }
            set
            {
                this.Set("EditableMedicine", ref this.editableMedicine, value);
            }
        }

        public Visibility Visibility 
        {
            get
            {
                return this.visibility;
            }
            set
            {
                this.Set("Visibility", ref this.visibility, value);
            }
        }

        public DeleteMedicineCommand DeleteMedicineCommand
        {
            get => this.deleteMedicineCommand;
        }

        public EditMedicineCommand EditMedicineCommand
        {
            get => this.editMedicineCommand;
        }

        public MedicinesViewModel()
        {
            this.Visibility = User.Default.CurrentProfile == "ministry_worker"? Visibility.Visible : Visibility.Collapsed;
            this.deleteMedicineCommand = new DeleteMedicineCommand(this.medicines, this.DeleteMedicine, _ => true);
            this.editMedicineCommand = new EditMedicineCommand(this.medicines, this.EditMedicine, _ => true);
        }

        private async Task<bool> DeleteMedicine(string uri)
        {
            return await ((App)App.Current).MedicineClient.DeleteMedicineAsync(uri);
        }

        private async Task<bool> EditMedicine(Medicine medicine)
        {
            return await ((App)App.Current).MedicineClient.UpdateMedicineAsync(medicine);
        }
    }
}
