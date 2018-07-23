using Desktop.Models;
using System;
using System.Windows;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Command for patient signing up
    /// </summary>
    public class PatientSignUpCommand : CommandBase
    {
        /// <summary>
        /// User Management API client
        /// </summary>
        private UserManagementApiClient _userManagementApiClient;

        /// <summary>
        /// Creates new instance of <see cref="RegisterCommand"/>
        /// </summary>
        public PatientSignUpCommand()
        {
            // setting fields
            this._userManagementApiClient = new UserManagementApiClient();
        }

        /// <summary>
        /// Determines if command can be executed
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating if the command can be executed</returns>
        public override bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;

            var patient = (PatientInfo)parameter;

            return ValidateValues(patient);
        }

        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter">parameter</param>
        public override async void ExecuteAsync(object parameter)
        {
            var patient = (PatientInfo)parameter;

            try
            {
                var info = await this._userManagementApiClient.PostPatientAsync(this.Map(patient));
                //if (info.Result.IsSuccessStatusCode)
                //    new CodeConfirmation(new ConfirmationViewModel(patient.Username)).Show();

            }
            catch (Exception)
            {
                MessageBox.Show("Error occured");
            }
        }

        /// <summary>
        /// Validates values
        /// </summary>
        /// <param name="patient">patient info</param>
        /// <returns>boolean value indicating the validity of values</returns>
        private bool ValidateValues(PatientInfo patient)
        {
            return (string.IsNullOrEmpty(patient.RegionalDoctorName) ||
                   string.IsNullOrEmpty(patient.Occupation) ||
                   string.IsNullOrEmpty(patient.Address)) == false;
        }

        /// <summary>
        /// Maps patient info
        /// </summary>
        /// <param name="patient">patient info</param>
        /// <returns>instance of <see cref="Patient"/> with the given patient information</returns>
        private Patient Map(PatientInfo patient)
        {
            return new Patient
            {
                // GET FROM ?
                UserId = patient.UserId,
                RegionalDoctorName = patient.RegionalDoctorName,
                Occupation = patient.Occupation,
                Address = patient.Address,
                IsAlcoholic = patient.IsAlcoholic,
                IsDrugAddicted = patient.IsDrugAddicted
            };
        }
    }
}
