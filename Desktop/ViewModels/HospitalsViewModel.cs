using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using InstitutionClient.Models;
using Desktop.Services;
using Desktop.Validations;
using Desktop.Commands;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Hospitals page viewmodel
    /// </summary>
    public class HospitalsViewModel : FilterablePageViewModel
    {
        /// <summary>
        /// Hospitals
        /// </summary>
        private ObservableCollection<Institution> hospitals;

        /// <summary>
        /// Editable visibility
        /// </summary>
        private Institution editableHospital;

        /// <summary>
        /// Edit or delete visibility
        /// </summary>
        private Visibility visibility;

        /// <summary>
        /// Editable institution validation
        /// </summary>
        private EditableInstitutionValidation validation;

        /// <summary>
        /// Filter service
        /// </summary>
        private readonly FilterService<Institution> filterService;

        /// <summary>
        /// Hospitals data
        /// </summary>
        private IEnumerable<Institution> _data;

        /// <summary>
        /// Hospital delete command
        /// </summary>
        private readonly DeleteHospitalCommand deleteHospitalCommand;

        /// <summary>
        /// Hospital edit command
        /// </summary>
        private readonly EditHospitalCommand editHospitalCommand;

        /// <summary>
        /// Hospitals load service
        /// </summary>
        private readonly LoadHospitalsService _loadHospitalsService;

        /// <summary>
        /// Load command
        /// </summary>
        private readonly LoadCommand _loadCommand;

        /// <summary>
        /// Filter command
        /// </summary>
        private readonly FilterCommand _filterCommand;

        /// <summary>
        /// Gets or sets hospitals
        /// </summary>
        public ObservableCollection<Institution> Hospitals
        {
            // gets hospitals
            get => this.hospitals;

            // sets hospitals
            set => this.Set("Hospitals", ref this.hospitals, value);
        }

        /// <summary>
        /// Gets or sets editable hospital
        /// </summary>
        public Institution EditableHospital
        {
            // gets editable hospital
            get => this.editableHospital;

            // sets editable hospital
            set => this.Set("EditableHospital", ref this.editableHospital, value);
        }

        /// <summary>
        /// Gets or sets edit or delete buttons visibility
        /// </summary>
        public Visibility Visibility
        {
            // gets visibility
            get => this.visibility;

            // sets visibility
            set => this.Set("Visibility", ref this.visibility, value);
        }
        
        /// <summary>
        /// Gets or sets hospitals data
        /// </summary>
        public IEnumerable<Institution> Data
        {
            // gets data
            get => this._data;

            // sets data
            set => this._data = value;
        }

        /// <summary>
        /// Gets hospital delete command
        /// </summary>
        public ICommand DeleteHospitalCommand => this.deleteHospitalCommand;

        /// <summary>
        /// Gets hospital edit command
        /// </summary>
        public ICommand EditHospitalCommand => this.editHospitalCommand;

        /// <summary>
        /// Gets load command
        /// </summary>
        public ICommand LoadCommand => this._loadCommand;

        /// <summary>
        /// Gets filter command
        /// </summary>
        public ICommand FilterCommand => this._filterCommand;

        /// <summary>
        /// Creates new instance of <see cref="HospitalsViewModel"/>
        /// </summary>
        public HospitalsViewModel() 
        {
            // setting visibility
            this.Visibility = (User.Default.CurrentProfile == "ministry_worker") || 
                              (User.Default.CurrentProfile == "hospital_admin") ? Visibility.Visible : Visibility.Collapsed;

            // initializing services
            this.filterService = new FilterService<Institution>();
            this._loadHospitalsService = new LoadHospitalsService(this);

            // initializing validations
            this.validation = new EditableInstitutionValidation();

            // initializing commands
            this.deleteHospitalCommand = new DeleteHospitalCommand(this, this.DeleteHospital, _ => true);
            this.editHospitalCommand = new EditHospitalCommand(this.hospitals, this.EditHospital, _ => true);   
            this._loadCommand = new LoadCommand(this._loadHospitalsService);

            this._filterCommand = new FilterCommand(this);

            ((App)App.Current).ProfileChanged += this.UpdateVisibiliies;
        }

        /// <summary>
        /// Updates hospitals
        /// </summary>
        public void UpdateVisibiliies()
        {
            this.Visibility = (User.Default.CurrentProfile == "ministry_worker") || 
                (User.Default.CurrentProfile == "hospital_admin") ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Filters content
        /// </summary>
        /// <returns>nothing</returns>
        public override async Task Filter()
        {
            // default predicate
            var defaultPredicate = new Func<Institution, bool>(inst => true);

            // name predicate
            var namePredicate = defaultPredicate;

            // address predicate
            var addressPredicate = defaultPredicate;

            // combined predicate
            var combinedPredicate = defaultPredicate;

            /* initializing predicates */

            if (!string.IsNullOrEmpty(this.Name))
                namePredicate = inst => inst.Name.Contains(this.Name);

            if (!string.IsNullOrEmpty(this.Address))
                addressPredicate = inst => inst.Address.Contains(this.Address);

            // combining predicates
            combinedPredicate = inst => namePredicate(inst) && addressPredicate(inst);

            // filtering hospitals
            var hospitals = await this.filterService.FilterAsync(this._data, combinedPredicate);

            // setting filtered hospitals
            this.Hospitals = new ObservableCollection<Institution>(hospitals);
        }

        /// <summary>
        /// Deletes hospital
        /// </summary>
        /// <param name="id">Hospital id</param>
        /// <returns>boolean value indicating if the hospital has successfully been deleted</returns>
        private async Task<bool> DeleteHospital(int id)
        {
            return await ((App)App.Current).InstitutionClient.DeleteInstitutionAsync(id);
        }

        /// <summary>
        /// Edits hospital
        /// </summary>
        /// <param name="hospital">Hospital</param>
        /// <returns>boolean value indicating if the hospital has successfully been edited</returns>
        private async Task<bool> EditHospital(Institution hospital)
        {
            // validating
            if (!this.validation.Validate(hospital))
            {
                return false;
            }

            // returning value
            return await ((App)App.Current).InstitutionClient.UpdateInstitutionAsync(hospital);
        }
    }
}