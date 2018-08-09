using Desktop.Interfaces;
using InstitutionClient.Models;

namespace Desktop.Validations
{
    public class EditableInstitutionValidation : IValidation
    {
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var institution = parameter as Institution;

            return !(institution.Name == null &&
                    institution.License == null &&
                    institution.Owner == null &&
                    institution.Phone == null &&
                    institution.OpenTime == null &&
                    institution.CloseTime == null &&
                    institution.Address == null &&
                    institution.Description == null &&
                    institution.Type == null);
        }
    }
}
