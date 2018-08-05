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
    /// View model for adding patient profile page
    /// </summary>
    public class PatientProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// Patient info
        /// </summary>
        private Patient patient;

        /// <summary>
        /// Validation
        /// </summary>
        private readonly IValidation validation;

        /// <summary>
        /// Patient profile service
        /// </summary>
        private readonly IService<Response<string>> patientProfileService;

        // Gets or sets patient info
        public Patient Patient
        {
            get => this.patient;

            set => this.Set("Patient", ref this.patient, value);
        }

        /// <summary>
        /// Patient profile command
        /// </summary>
        private readonly ProfileCommand<Patient> patientProfileCommand;

        /// <summary>
        /// Gets patient command
        /// </summary>
        public ICommand PatientProfileCommand => this.patientProfileCommand;
        
        /// <summary>
        /// Creates new instance of <see cref="AddPatientProfile"/>
        /// </summary>
        public PatientProfileViewModel()
        {
            // setting fields
            this.patient = new Patient();
            this.patient.UserId = User.Default.Id;
            this.validation = new PatientInputValidation();
            this.patientProfileService = new PatientProfileService();
            this.patientProfileCommand = new ProfileCommand<Patient>(
                this.patientProfileService.Execute, 
                this.validation.Validate,
                "patient");
        }
    }
}

