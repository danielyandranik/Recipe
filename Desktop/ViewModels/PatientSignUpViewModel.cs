using Desktop.Commands;
using Desktop.Models;
using Desktop.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model for patient sign up page
    /// </summary>
    public class PatientSignUpViewModel : ViewModelBase
    {
        /// <summary>
        /// Patient info
        /// </summary>
        private PatientInfo _patient;

        // Gets or sets patient info
        public PatientInfo Patient
        {
            get => this._patient;

            set => this.SetProperty(ref this._patient, value);
        }

        /// <summary>
        /// Gets or sets Patient Sign Up command
        /// </summary>
        public ICommand PatientSignUpCommand { get; private set; }

        /// <summary>
        /// Gets or sets Add patient profile page
        /// </summary>
        public AddPatientProfile PatientProfile { get; private set; }

        /// <summary>
        ///  Creates new instance of <see cref="PatientSignUpViewModel"/>
        /// </summary>
        /// <param name="patientProfile">patient profile info</param>
        public PatientSignUpViewModel(AddPatientProfile patientProfile)
        {
            // setting fields and properties
            this._patient = new PatientInfo();
            this.PatientProfile = patientProfile;
            this.PatientSignUpCommand = new PatientSignUpCommand();
        }
    }
}
