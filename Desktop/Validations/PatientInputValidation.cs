using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    /// <summary>
    /// Patient input validation
    /// </summary>
    public class PatientInputValidation : IValidation
    {
        /// <summary>
        /// Validates patient input
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating the validity of patient input</returns>
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var patient = (Patient)parameter;

            return !(string.IsNullOrEmpty(patient.RegionalDoctorName) ||
                     string.IsNullOrEmpty(patient.Occupation) ||
                     string.IsNullOrEmpty(patient.Address));
        }
    }
}