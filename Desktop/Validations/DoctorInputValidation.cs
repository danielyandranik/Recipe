using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    /// <summary>
    /// Doctor input validation
    /// </summary>
    public class DoctorInputValidation : IValidation
    {
        /// <summary>
        /// Validates doctor input
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating the validity of doctor input</returns>
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
