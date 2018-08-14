using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using Desktop.Commands;
using System.Collections.ObjectModel;
using InstitutionClient.Models;
using System.Windows;
using Desktop.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Desktop.Validations;
using Desktop.Models;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model for Pharmacies page
    /// </summary>
    public class PharmaciesViewModel : FilterablePageViewModel
    {
        /// <summary>
        /// Container for pharmacies
        /// </summary>
        private ObservableCollection<Institution> pharmacies;

        /// <summary>
        /// Container for medicines
        /// </summary>
        private ObservableCollection<MedicinePricePair> medicines;

        /// <summary>
        /// Editably pharmacy
        /// </summary>
        private Institution editablePharmacy;

        /// <summary>
        /// Visibility of some features
        /// </summary>
        private Visibility visibility;

        /// <summary>
        /// Validation
        /// </summary>
        private EditableInstitutionValidation validation;

        /// <summary>
        /// Filter service for institutions
        /// </summary>
        private readonly FilterService<Institution> filterService;

        /// <summary>
        /// All loaded data
        /// </summary>
        public IEnumerable<Institution> data;

        /// <summary>
        /// Load service
        /// </summary>
        private readonly LoadPharmaciesService _loadPharmaciesService;

        /// <summary>
        /// Command for deleting a pharmacy
        /// </summary>
        private readonly DeletePharmacyCommand deletePharmacyCommand;

        /// <summary>
        /// Command for editing a pharmacy
        /// </summary>
        private readonly EditPharmacyCommand editPharmacyCommand;

        /// <summary>
        /// Load Command
        /// </summary>
        private readonly LoadCommand _loadCommand;

        /// <summary>
        /// Filter command
        /// </summary>
        private readonly FilterCommand _filterCommand;

        /// <summary>
        /// Gets or sets pharmacies value
        /// </summary>
        public ObservableCollection<Institution> Pharmacies
        {
            // gets pharmacies 
            get => this.pharmacies;

            // sets pharmacies
            set => this.Set("Pharmacies", ref this.pharmacies, value);
        }

        /// <summary>
        /// Gets or sets medicines value
        /// </summary>
        public ObservableCollection<MedicinePricePair> Medicines
        {
            // gets medicines
            get => this.medicines;

            // sets medicines
            set => this.Set("Medicines", ref this.medicines, value);
        }

        /// <summary>
        /// Gets or sets editable pharmacy value
        /// </summary>
        public Institution EditablePharmacy
        {
            // gets editable pharmacy
            get => this.editablePharmacy;

            // sets editable pharmacy
            set => this.Set("EditablePharmacy", ref this.editablePharmacy, value);
        }

        /// <summary>
        /// Gets or sets visibility value 
        /// </summary>
        public Visibility Visibility
        {
            // gets visibility
            get => this.visibility;

            // sets visibility
            set => this.Set("Visibility", ref this.visibility, value);
        }

        /// <summary>
        /// Getter for delete pharmacy command
        /// </summary>
        public ICommand DeletePharmacyCommand  => this.deletePharmacyCommand;

        /// <summary>
        /// Getter for edit pharmacy command
        /// </summary>
        public ICommand EditPharmacyCommand => this.editPharmacyCommand;

        /// <summary>
        /// Get load pharmacies command
        /// </summary>
        public ICommand LoadPharmacies => this._loadCommand;

        /// <summary>
        /// Gets filter command
        /// </summary>
        public ICommand FilterCommand => this._filterCommand;

        /// <summary>
        /// Creates a new instanse of <see cref="PharmaciesViewModel"/>
        /// </summary>
        public PharmaciesViewModel()
        {
            // setting visibility
            this.Visibility = (User.Default.CurrentProfile == "ministry_worker") || 
                (User.Default.CurrentProfile == "pharmacy_admin") ? Visibility.Visible : Visibility.Collapsed;

            // initializing services
            this.filterService = new FilterService<Institution>();
            this.validation = new EditableInstitutionValidation();
            this._loadPharmaciesService = new LoadPharmaciesService(this);

            // initializing commands
            this.deletePharmacyCommand = new DeletePharmacyCommand(this, this.DeletePharmacy, _ => true);
            this.editPharmacyCommand = new EditPharmacyCommand(this, this.EditPharmacy, _ => true);
            this._loadCommand = new LoadCommand(this._loadPharmaciesService);
            this._filterCommand = new FilterCommand(this);

            // registering Updater to Profile Changed event
            ((App)App.Current).ProfileChanged += this.UpdateVisibilities;
        }

        /// <summary>
        /// Updates visibilities
        /// </summary>
        private void UpdateVisibilities()
        {
            this.Visibility = (User.Default.CurrentProfile == "ministry_worker") || 
                (User.Default.CurrentProfile == "pharmacy_admin") ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Filters data by given predicate
        /// </summary>
        /// <returns>nothing</returns>
        public async override Task Filter()
        {
            // defaul predicate
            var defaultPredicate = new Func<Institution, bool>(inst => true);

            // name predicate
            var namePredicate = defaultPredicate;

            // address predicate
            var addressPredicate = defaultPredicate;

            // combined predicate
            var combinedPredicate = defaultPredicate;

            /* constructing predicates if there is need */

            if (!string.IsNullOrEmpty(this.Name))
                namePredicate = inst => inst.Name.Contains(this.Name);

            if (!string.IsNullOrEmpty(this.Address))
                addressPredicate = inst => inst.Address.Contains(this.Address);

            // combining predicates
            combinedPredicate = inst => namePredicate(inst) && addressPredicate(inst);

            // filtering
            var pharmacies = await this.filterService.FilterAsync(this.data, combinedPredicate);

            // setting filtered pharmacies
            this.Pharmacies = new ObservableCollection<Institution>(pharmacies);
        }

        /// <summary>
        /// Delete pharmacy with specified id
        /// </summary>
        /// <param name="id">Pharmacy Id</param>
        /// <returns>Boolean value indicating the success of operation</returns>
        private async Task<bool> DeletePharmacy(int id)
        {
            return await ((App)App.Current).InstitutionClient.DeleteInstitutionAsync(id);
        }

        /// <summary>
        /// Edit pharmacy with given values
        /// </summary>
        /// <param name="pharmacy">Pharmacy info</param>
        /// <returns>Boolean value indicating the success of operation</returns>
        private async Task<bool> EditPharmacy(Institution pharmacy)
        {
            // validating
            if (!this.validation.Validate(pharmacy))
            {
                return false;
            }

            // returning value
            return await ((App)App.Current).InstitutionClient.UpdateInstitutionAsync(pharmacy);
        }
    }
}
