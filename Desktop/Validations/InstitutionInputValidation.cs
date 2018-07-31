using Desktop.Interfaces;
using InstitutionClient.Models;
using System;
using System.Linq;
using System.Net.Mail;

namespace Desktop.Validations
{
    public class InstitutionInputValidation : IValidation
    {
        /// <summary>
        /// Validates the parameter
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of parameter input</returns>
        public bool Validate(object parameter)
        {
            if (parameter == null)
                return false;

            var institution = parameter as Institution;

            return this.ValidateEmail(institution) &&
                    this.ValidatePhone(institution) &&
                    this.ValidateTimes(institution) &&
                    this.ValidateType(institution) &&
                    this.ValidateValues(institution);
        }

        /// <summary>
        /// Validation of open and close times
        /// </summary>
        /// <param name="input">parameter</param>
        /// <returns>boolean value indicating the validity of times</returns>
        private bool ValidateTimes(Institution institution)
        {
            TimeSpan output;
            return TimeSpan.TryParse(institution.OpenTime, out output) &&
                    TimeSpan.TryParse(institution.CloseTime, out output);
        }

        /// <summary>
        /// Validation of some values
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of some values</returns>
        private bool ValidateValues(Institution institution)
        {
            return !(string.IsNullOrEmpty(institution.Name) ||
                   string.IsNullOrEmpty(institution.License) ||
                   string.IsNullOrEmpty(institution.Owner) ||
                   string.IsNullOrEmpty(institution.Description) ||
                   string.IsNullOrEmpty(institution.Address));
        }

        /// <summary>
        /// Validation of the phone number
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of phone number input</returns>
        private bool ValidatePhone(Institution institution)
        {
            if (string.IsNullOrEmpty(institution.Phone))
                return false;

            return institution.Phone.All(char.IsDigit);
        }

        /// <summary>
        /// Validation of the email
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of email input</returns>
        private bool ValidateEmail(Institution institution)
        {
            if (string.IsNullOrEmpty(institution.Email))
                return false;

            try
            {
                new MailAddress(institution.Email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validation of the type
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of type input</returns>
        private bool ValidateType(Institution institution)
        {
            return institution?.Type != null;
        }

    }
}
