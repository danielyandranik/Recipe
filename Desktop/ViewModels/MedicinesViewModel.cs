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

namespace Desktop.ViewModels
{
    public class MedicinesViewModel : ViewModelBase
    {
        private ObservableCollection<Medicine> _medicines;

        private Medicine _editableMedicine;

        private bool _isVisible;

        private DeleteMedicineCommand _deleteMedicineCommand;

        private EditMedicineCommand _editMedicineCommand;

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

        public Medicine EditableMedicine
        {
            get
            {
                return this._editableMedicine;
            }
            set
            {
                this.Set("EditableMedicine", ref this._editableMedicine, value);
            }
        }

        public bool IsVisible
        {
            get
            {
                return this._isVisible;
            }
            set
            {
                this.Set("IsVisible", ref this._isVisible, value);
            }
        }


        public MedicinesViewModel()
        {
            this.LoadMedicines();
            this._isVisible = User.Default.CurrentProfile == "ministry_worker" ? true : false;
            this._deleteMedicineCommand = new DeleteMedicineCommand(this._medicines, this.deleteMedicine, _ => true);
            this._editMedicineCommand = new EditMedicineCommand(this._medicines, this.editMedicine, _ => true);
        }

        private async Task<bool> deleteMedicine(string uri)
        {
            return await ((App)App.Current).MedicineClient.DeleteMedicineAsync(uri);
        }

        private async Task<bool> editMedicine(Medicine medicine)
        {
            return await ((App)App.Current).MedicineClient.UpdateMedicineAsync(medicine);
        }

        private void LoadMedicines()
        {
            var response = ((App)App.Current).MedicineClient.GetAllMedicinesAsync("api/medicines").Result;
            if(!response.IsSuccessStatusCode)
            {
                Debug.Write(response.StatusCode);
                return;
            }

            this.Medicines = new ObservableCollection<Medicine>(response.Result);
        }

    }
}
