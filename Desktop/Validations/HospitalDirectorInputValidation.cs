using Desktop.Interfaces;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    public class HospitalDirectorInputValidation : IValidation
    {
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var director = (HospitalDirector)parameter;

            int temp;
            return !(string.IsNullOrEmpty(director.HospitalName) ||
                        string.IsNullOrEmpty(director.Occupation) ||
                            !int.TryParse(director.StartedWorking, out temp));
        }
    }
}
