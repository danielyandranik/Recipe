using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    /// <summary>
    /// Validation for confirmation code
    /// </summary>
    public class ConfirmationCodeInputValidation : IValidation
    {
        /// <summary>
        /// Validates the given parameter
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of parameter</returns>
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            return ((UserVerificationInfo)parameter).VerifyKey.Length > 31;
        }
    }
}
