using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    class PatientInputValidation : IValidation
    {
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var patient = (Patient)parameter;

            return (string.IsNullOrEmpty(patient.RegionalDoctorName) ||
          string.IsNullOrEmpty(patient.Occupation) ||
          string.IsNullOrEmpty(patient.Address)) == false;
        }
    }
}