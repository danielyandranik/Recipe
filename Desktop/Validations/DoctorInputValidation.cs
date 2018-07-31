using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    public class DoctorInputValidation : IValidation
    {
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var doctor = parameter as Doctor;

            int temp;
            return !(string.IsNullOrEmpty(doctor.License) ||
                string.IsNullOrEmpty(doctor.Specification) ||
                string.IsNullOrEmpty(doctor.HospitalName) ||
                !int.TryParse(doctor.GraduatedYear, out temp) ||
                !int.TryParse(doctor.WorkStartYear, out temp));

        }
    }
}
