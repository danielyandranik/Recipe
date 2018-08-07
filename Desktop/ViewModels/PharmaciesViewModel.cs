using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using Desktop.Commands;
using System.Collections.ObjectModel;
using InstitutionClient.Models;
using System.Windows;
using Desktop.Services;
using System;
using System.Collections.Generic;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model for Pharmacies page
    /// </summary>
    public class PharmaciesViewModel : ViewModelBase
    {
        /// <summary>
        /// Container for pharmacies
        /// </summary>
        private ObservableCollection<Institution> pharmacies;

        /// <summary>
        /// Editably pharmacy
        /// </summary>
        private Institution editablePharmacy;

        /// <summary>
        /// Visibility of some features
        /// </summary>
        private Visibility visibility;

        /// <summary>
        /// Filter service for institutions
        /// </summary>
        private readonly FilterService<Institution> filterService;

        /// <summary>
        /// All loaded data
        /// </summary>
        public IEnumerable<Institution> data;

        /// <summary>
        /// Command for deleting a pharmacy
        /// </summary>
        private readonly DeletePharmacyCommand deletePharmacyCommand;

        /// <summary>
        /// Command for editing a pharmacy
        /// </summary>
        private readonly EditPharmacyCommand editPharmacyCommand;

        /// <summary>
        /// Gets or sets pharmacies value
        /// </summary>
        public ObservableCollection<Institution> Pharmacies
        {
            get
            {
                return this.pharmacies;
            }
            set
            {
                this.Set("Pharmacies", ref this.pharmacies, value);
            }
        }

        /// <summary>
        /// Gets or sets editable pharmacy value
        /// </summary>
        public Institution EditablePharmacy
        {
            get
            {
                return this.editablePharmacy;
            }
            set
            {
                this.Set("EditablePharmacy", ref this.editablePharmacy, value);
            }
        }

        /// <summary>
        /// Gets or sets visibility value 
        /// </summary>
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

        /// <summary>
        /// Getter for delete pharmacy command
        /// </summary>
        public DeletePharmacyCommand DeletePharmacyCommand
        {
            get => this.deletePharmacyCommand;
        }

        /// <summary>
        /// Getter for edit pharmacy command
        /// </summary>
        public EditPharmacyCommand EditPharmacyCommand
        {
            get => this.editPharmacyCommand;
        }

        /// <summary>
        /// Creates a new instanse of <see cref="PharmaciesViewModel"/>
        /// </summary>
        public PharmaciesViewModel()
        {
            this.Visibility = (User.Default.CurrentProfile == "ministry_worker") || (User.Default.CurrentProfile == "pharmacy_admin") ? Visibility.Visible : Visibility.Collapsed;
            this.deletePharmacyCommand = new DeletePharmacyCommand(this.pharmacies, this.deletePharmacy, _ => true);
            this.editPharmacyCommand = new EditPharmacyCommand(this.pharmacies, this.editPharmacy, _ => true);
            this.filterService = new FilterService<Institution>();
        }

        /// <summary>
        /// Filters data by given predicate
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns></returns>
        public async Task Filter(Func<Institution, bool> predicate)
        {
            var pharmacies = await this.filterService.FilterAsync(this.data, predicate);

            this.Pharmacies = new ObservableCollection<Institution>(pharmacies);
        }

        /// <summary>
        /// Delete pharmacy with specified id
        /// </summary>
        /// <param name="id">Pharmacy Id</param>
        /// <returns>Boolean value indicating the success of operation</returns>
        private async Task<bool> deletePharmacy(int id)
        {
            return await ((App)App.Current).InstitutionClient.DeleteInstitutionAsync(id);
        }

        /// <summary>
        /// Edit pharmacy with given values
        /// </summary>
        /// <param name="pharmacy">Pharmacy info</param>
        /// <returns>Boolean value indicating the success of operation</returns>
        private async Task<bool> editPharmacy(Institution pharmacy)
        {
            return await ((App)App.Current).InstitutionClient.UpdateInstitutionAsync(pharmacy);
        }
    }
}
