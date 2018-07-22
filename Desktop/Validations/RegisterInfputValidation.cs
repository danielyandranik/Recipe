using System;
using System.Linq;
using System.Net.Mail;
using Desktop.Interfaces;
using Desktop.Models;

namespace Desktop.Validations
{
    /// <summary>
    /// Register input validation
    /// </summary>
    public class RegisterInfputValidation:IValidation
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

            var register = (Register)parameter;

            return this.ValidateDate(register) &&
                    this.ValidatePassword(register) &&
                    this.ValidatePhone(register) &&
                    this.ValidateNames(register) &&
                    this.ValidateEmail(register) &&
                    this.ValidateSex(register);
        }

        /// <summary>
        /// Validates the date
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of date input</returns>
        private bool ValidateDate(Register register)
        {
            if (!(int.TryParse(register.Year, out var year) &&
                int.TryParse(register.Month, out var month) &&
                int.TryParse(register.Day, out var day)))
                return false;

            return (year > 1899 && year <= DateTime.Now.Year && month > 0 && month < 13 && day > 0 && day < 32);
        }

        /// <summary>
        /// Validates the names
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of names input</returns>
        private bool ValidateNames(Register register)
        {
            return (string.IsNullOrEmpty(register.FirstName) ||
                   string.IsNullOrEmpty(register.LastName) ||
                   string.IsNullOrEmpty(register.MiddleName) ||
                   string.IsNullOrEmpty(register.Username)) == false;
        }

        /// <summary>
        /// Validates the password
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of password input</returns>
        private bool ValidatePassword(Register register)
        {
            if (string.IsNullOrEmpty(register.Password) || string.IsNullOrEmpty(register.ConfirmPassword))
                return false;

            return register.Password.Length > 7 && register.ConfirmPassword.Length > 7
                && register.Password == register.ConfirmPassword;
        }

        /// <summary>
        /// Validates the phone number
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of phone number input</returns>
        private bool ValidatePhone(Register register)
        {
            if (string.IsNullOrEmpty(register.Phone))
                return false;

            return register.Phone.All(char.IsDigit);
        }

        /// <summary>
        /// Validates the sex
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of sex input</returns>
        private bool ValidateSex(Register register)
        {
            return register?.SexIndex != null;
        }

        /// <summary>
        /// Validates the email
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>boolean value indicating the validity of email input</returns>
        private bool ValidateEmail(Register register)
        {
            if (string.IsNullOrEmpty(register.Email))
                return false;

            try
            {
                new MailAddress(register.Email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
