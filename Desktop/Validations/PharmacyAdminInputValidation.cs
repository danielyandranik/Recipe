using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    /// <summary>
    /// Pharmacy admin input validation
    /// </summary>
    public class PharmacyAdminInputValidation : IValidation
    {
        /// <summary>
        /// Validates the pharmacy admin input
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating the validity of pharmacy admin input.</returns>
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var pharmacyAdmin = parameter as PharmacyAdmin;

            return !string.IsNullOrEmpty(pharmacyAdmin.PharmacyName) &&
                    int.TryParse(pharmacyAdmin.StartedWorkingYear, out var temp);
        }
    }
}
