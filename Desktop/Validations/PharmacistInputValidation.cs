using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    public class PharmacistInputValidation : IValidation
    {
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var patient = (PharmacistFullInfo)parameter;

            int temp;
            return (!string.IsNullOrEmpty(patient.PharmacyName) &&
                        int.TryParse(patient.StartedWorking, out temp));

        }
    }
}
