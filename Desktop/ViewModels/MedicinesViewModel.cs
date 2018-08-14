using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MedicineApiClient;
using Desktop.Commands;
using Desktop.Services;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Medicines page view model
    /// </summary>
    public class MedicinesViewModel : FilterablePageViewModel
    {
        /// <summary>
        /// Medicines
        /// </summary>
        private ObservableCollection<Medicine> medicines;

        /// <summary>
        /// Editable medicine
        /// </summary>
        private Medicine editableMedicine;

        /// <summary>
        /// Edit and delete buttons visibility
        /// </summary>
        private Visibility visibility;

        /// <summary>
        /// Filter service
        /// </summary>
        private readonly FilterService<Medicine> _filterService;

        /// <summary>
        /// Medicine data
        /// </summary>
        private IEnumerable<Medicine> _data;

        /// <summary>
        /// Medicine name
        /// </summary>
        private string _medicineName;

        /// <summary>
        /// Country
        /// </summary>
        private string _country;

        /// <summary>
        /// Delete medicine command
        /// </summary>
        private readonly DeleteMedicineCommand _deleteMedicineCommand;

        /// <summary>
        /// Edit medicine command
        /// </summary>
        private readonly EditMedicineCommand _editMedicineCommand;

        /// <summary>
        /// Filter command
        /// </summary>
        private readonly FilterCommand _filterCommand;

        /// <summary>
        /// Gets or sets medicines
        /// </summary>
        public ObservableCollection<Medicine> Medicines
        {
            // gets medicines
            get => this.medicines;

            // sets medicines
            set => this.Set("Medicines", ref this.medicines, value);
        }

        /// <summary>
        /// Gets or sets editable medicine
        /// </summary>
        public Medicine EditableMedicine
        {
            // gets editable medicine
            get => this.editableMedicine;

            // sets editable medicine
            set => this.Set("EditableMedicine", ref this.editableMedicine, value);
        }

        /// <summary>
        /// Gets or sets edit and delete button visibility
        /// </summary>
        public Visibility Visibility 
        {
            // gets visibility
            get => this.visibility;

            // sets visibility
            set => this.Set("Visibility", ref this.visibility, value);
        }

        /// <summary>
        /// Gets or sets medicine name
        /// </summary>
        public string MedicineName
        {
            // gets medicine name
            get => this._medicineName;

            // sets medicine name
            set => this.Set("MedicineName", ref this._medicineName, value);
        }

        /// <summary>
        /// Gets or sets country
        /// </summary>
        public string Country
        {
            // gets country
            get => this._country;

            // sets country
            set => this.Set("Country", ref this._country, value);
        }

        /// <summary>
        /// Gets or sets medicine date
        /// </summary>
        public IEnumerable<Medicine> Data
        {
            // gets medicine data
            get => this._data;

            // sets medicine data
            set => this._data = value;
        }

        /// <summary>
        /// Gets delete medicine command
        /// </summary>
        public ICommand DeleteMedicineCommand => this._deleteMedicineCommand;        

        /// <summary>
        /// Gets edit medicine command
        /// </summary>
        public ICommand EditMedicineCommand => this._editMedicineCommand;

        /// <summary>
        /// Gets filter command
        /// </summary>
        public ICommand FilterCommand => this._filterCommand;

        /// <summary>
        /// Creates new instance of <see cref="MedicinesViewModel"/>
        /// </summary>
        public MedicinesViewModel()
        {
            // setting visibilities
            this.Visibility = User.Default.CurrentProfile == "ministry_worker"? Visibility.Visible : Visibility.Collapsed;

            // initializing services
            this._filterService = new FilterService<Medicine>();

            // initialing commands
            this._deleteMedicineCommand = new DeleteMedicineCommand(this, this.DeleteMedicine, _ => true);
            this._editMedicineCommand = new EditMedicineCommand(this, this.EditMedicine, _ => true);
            this._filterCommand = new FilterCommand(this);

            // registering update visibilitis to ProfileChanged event
            ((App)App.Current).ProfileChanged += this.UpdateVisibilities;
        }

        /// <summary>
        /// Filters medicines
        /// </summary>
        /// <returns></returns>
        public async override Task Filter()
        {
            // defualt predicate
            var defualtPredicate = new Func<Medicine, bool>(med => true);

            // med name predicate
            var medNamePredicate = defualtPredicate;

            // med country predicate
            var medCountryPredicate = defualtPredicate;

            // combined predicate
            var combinedPredicate = defualtPredicate;

            /* constructing predicates if there is need */

            if (!string.IsNullOrEmpty(this.MedicineName))
                medNamePredicate = med => med.Name.Contains(this.MedicineName);

            if (!string.IsNullOrEmpty(this.Country))
                medCountryPredicate = med => med.Country.Contains(this.Country);

            // combining predicates
            combinedPredicate = med => medNamePredicate(med) && medCountryPredicate(med);

            // filtering medicines
            var medicines = await this._filterService.FilterAsync(this._data, combinedPredicate);

            // setting filtered medicines
            this.Medicines = new ObservableCollection<Medicine>(medicines);
        }

        /// <summary>
        /// Updates visibilities
        /// </summary>
        public void UpdateVisibilities()
        {
            this.Visibility = User.Default.CurrentProfile == "ministry_worker" ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Deletes medicine.
        /// </summary>
        /// <param name="uri">Uri</param>
        /// <returns>boolean value indicating whether medicine has been successfully deleted</returns>
        private async Task<bool> DeleteMedicine(string uri)
        {
            return await ((App)App.Current).MedicineClient.DeleteMedicineAsync(uri);
        }

        /// <summary>
        /// Edits medicines
        /// </summary>
        /// <param name="medicine">medicine</param>
        /// <returns>boolean value indicating wheter the medicine has been successfully edited</returns>
        private async Task<bool> EditMedicine(Medicine medicine)
        {
            return await ((App)App.Current).MedicineClient.UpdateMedicineAsync(medicine);
        }
    }
}
