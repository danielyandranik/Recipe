using Desktop.Commands;
using Desktop.Services;
using MedicineApiClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.ViewModels
{
    public class MedicinesViewModel : LoadablePageViewModel
    {
        private ObservableCollection<Medicine> medicines;

        private Medicine editableMedicine;

        private Visibility visibility;

        private readonly FilterService<Medicine> _filterService;

        public IEnumerable<Medicine> data;

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
            this._filterService = new FilterService<Medicine>();
            ((App)App.Current).ProfileChanged += this.UpdateVisibilities;
        }

        public async Task Filter(Func<Medicine,bool> predicate)
        {
            var medicines = await this._filterService.FilterAsync(this.data, predicate);

            this.Medicines = new ObservableCollection<Medicine>(medicines);
        }

        public void UpdateVisibilities()
        {
            this.Visibility = User.Default.CurrentProfile == "ministry_worker" ? Visibility.Visible : Visibility.Collapsed;
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
