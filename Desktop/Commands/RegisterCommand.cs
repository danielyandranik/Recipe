using System;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;
using Desktop.Models;
using Desktop.Views;
using Desktop.ViewModels;

namespace Desktop.Commands
{
    /// <summary>
    /// Command for registration
    /// </summary>
    public class RegisterCommand : CommandBase
    {
        /// <summary>
        /// User Management API client
        /// </summary>
        private UserManagementApiClient _userManagementApiClient;

        /// <summary>
        /// Creates new instance of <see cref="RegisterCommand"/>
        /// </summary>
        public RegisterCommand()
        {
            // setting fields
            this._userManagementApiClient = new UserManagementApiClient();
        }

        /// <summary>
        /// Determines if command can be executed.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>boolean value indicating if the command can be executed</returns>
        public override bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;

            var register = (RegisterInfo)parameter;

            return this.ValidateDate(register) &&
                    this.ValidatePassword(register) && 
                    this.ValidatePhone(register) &&
                    this.ValidateNames(register) &&
                    this.ValidateEmail(register) &&
                    this.ValidateSex(register);
        }

        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter">parameter</param>
        public override async void Execute(object parameter)
        {
            var register= (RegisterInfo)parameter;

            try
            {
                var info = await this._userManagementApiClient.RegisterAsync(this.Map(register));
                if (info.Result.IsSuccessStatusCode)
                    new CodeConfirmation(new ConfirmationViewModel(register.Username)).Show();                
            }
            catch(Exception)
            {
                MessageBox.Show("Error occured");
            }
        }

        /// <summary>
        /// Validates date
        /// </summary>
        /// <param name="register">register info</param>
        /// <returns>boolean value indicating the validity of date</returns>
        private bool ValidateDate(RegisterInfo register)
        {
            if (!(int.TryParse(register.Year, out var year) &&
                int.TryParse(register.Month, out var month) &&
                int.TryParse(register.Day, out var day)))
                return false;

            return (year > 1899 && year <= DateTime.Now.Year && month > 0 && month < 13 && day > 0 && day < 32);
        }

        /// <summary>
        /// Validates names
        /// </summary>
        /// <param name="register">register info</param>
        /// <returns>boolean value indicating the validity of names</returns>
        private bool ValidateNames(RegisterInfo register)
        {
            return (string.IsNullOrEmpty(register.FirstName) ||
                   string.IsNullOrEmpty(register.LastName) ||
                   string.IsNullOrEmpty(register.MiddleName) ||
                   string.IsNullOrEmpty(register.Username)) == false;
        }

        /// <summary>
        /// Validates password
        /// </summary>
        /// <param name="register">register info</param>
        /// <returns>boolean value indicating the validity of password</returns>
        private bool ValidatePassword(RegisterInfo register)
        {
            if (string.IsNullOrEmpty(register.Password) ||string.IsNullOrEmpty(register.ConfirmPassword))
                return false;

            return register.Password.Length > 7 && register.ConfirmPassword.Length > 7 
                && register.Password == register.ConfirmPassword;
        }

        /// <summary>
        /// Validates phone number
        /// </summary>
        /// <param name="register">register info</param>
        /// <returns>boolean value indicating the validity of phone number</returns>
        private bool ValidatePhone(RegisterInfo register)
        {
            if (string.IsNullOrEmpty(register.Phone))
                return false;

            return register.Phone.All(char.IsDigit);
        }

        /// <summary>
        /// Validates sex
        /// </summary>
        /// <param name="register">register info</param>
        /// <returns>boolean value indicating the validity of sex</returns>
        private bool ValidateSex(RegisterInfo register)
        {
            return register?.Sex?.Length != 0;
        }

        /// <summary>
        /// Validates mail address
        /// </summary>
        /// <param name="register">register info</param>
        /// <returns>boolean value indicating the validity of mail address</returns>
        private bool ValidateEmail(RegisterInfo register)
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

        /// <summary>
        /// Maps register info
        /// </summary>
        /// <param name="register">register info</param>
        /// <returns>instance of <see cref="UserRegisterInfo"/> with the given registration information</returns>
        private UserRegisterInfo Map(RegisterInfo register)
        {
            return new UserRegisterInfo
            {
                Username = register.Username,
                Email = register.Email,
                Sex = register.Sex.First().ToString(),
                FirstName = register.FirstName,
                LastName = register.LastName,
                MiddleName = register.MiddleName,
                Birthdate = $"{register.Year}-{register.Month}-{register.Day}",
                FullName = $"{register.FirstName} {register.MiddleName} {register.LastName}",
                Password = register.Password,
                Phone = register.Phone
            };
        }
    }
}