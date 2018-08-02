using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    /// <summary>
    /// Hospital director input validation
    /// </summary>
    public class HospitalDirectorInputValidation : IValidation
    {
        /// <summary>
        /// Validates hospital director input
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating the validity of hospital director input</returns>
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var director = (HospitalDirector)parameter;

            return !(string.IsNullOrEmpty(director.HospitalName) ||
                        string.IsNullOrEmpty(director.Occupation) ||
                            !int.TryParse(director.StartedWorking, out var temp));
        }
    }
}
