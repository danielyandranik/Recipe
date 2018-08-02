using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    /// <summary>
    /// Pharmacist input validation
    /// </summary>
    public class PharmacistInputValidation : IValidation
    {
        /// <summary>
        /// Validates pharmacist input
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating the validity of pharmacist input</returns>
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var patient = (PharmacistFullInfo)parameter;

            return (!string.IsNullOrEmpty(patient.PharmacyName) &&
                     int.TryParse(patient.StartedWorking, out var temp));

        }
    }
}
