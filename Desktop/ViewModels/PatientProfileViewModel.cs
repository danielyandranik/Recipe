using Desktop.Commands;
using Desktop.Interfaces;
using Desktop.Services;
using Desktop.Validations;
using Desktop.Views.Pages;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model for patient sign up page
    /// </summary>
    public class PatientProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// Patient info
        /// </summary>
        private Patient _patient;

        /// <summary>
        /// Patient id as a user
        /// </summary>
        private readonly int _userId;

        /// <summary>
        /// Validation
        /// </summary>
        private readonly IValidation _validation;

        /// <summary>
        /// Patient profile service
        /// </summary>
        private readonly IService<Response<string>> _patientProfileService;

        // Gets or sets patient info
        public Patient Patient
        {
            get => this._patient;

            set => this.Set("Patient", ref this._patient, value);
        }

        /// <summary>
        /// Patient profile command
        /// </summary>
        private readonly PatientProfileCommand _patientProfileCommand;

        /// <summary>
        /// Gets register commad
        /// </summary>
        public ICommand PatientProfileCommand => this._patientProfileCommand;

        /// <summary>
        /// Gets or sets Add patient profile page
        /// </summary>
        public AddPatientProfile PatientProfile { get; private set; }

        /// <summary>
        /// Creates new instance of <see cref="AddPatientProfile"/>
        /// </summary>
        public PatientProfileViewModel()
        {
            // setting fields
            this._userId = 1;
            this._patient = new Patient();
            this.Patient.UserId = 1;
            this._validation = new PatientInputValidation();
            this._patientProfileService = new PatientProfileService();
            this._patientProfileCommand = new PatientProfileCommand(this._patientProfileService.Execute, this._validation.Validate);
        }
    }
}

