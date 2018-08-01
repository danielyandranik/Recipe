using Desktop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Models;

namespace Desktop.Validations
{
    public class PharmacyAdminInputValidation : IValidation
    {
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var pharmacyAdmin = parameter as PharmacyAdmin;

            return !string.IsNullOrEmpty(pharmacyAdmin.PharmacyName) &&
                int.TryParse(pharmacyAdmin.StartedYearWorking, out var temp);
        }
    }
}
