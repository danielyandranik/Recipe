using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using UserManagementConsumer.Client;
using Desktop.Models;
using Desktop.Views.Windows;
using Desktop.ViewModels;

namespace Desktop.Commands
{
    /// <summary>
    /// Command for registration
    /// </summary>
    public class RegisterCommand : AsyncCommand<Register, Response<HttpResponseMessage>>
    {
        /// <summary>
        /// Register window view model
        /// </summary>
        private readonly RegisterWindowViewModel _vm;

        /// <summary>
        /// Boolean value for Sign Up button availability
        /// </summary>
        private bool _isSignUpAvailable;

        /// <summary>
        /// Creates new instance of <see cref="RegisterCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public RegisterCommand(RegisterWindowViewModel registerWindowViewModel,Func<Register, Task<Response<HttpResponseMessage>>> executeMethod, Func<Register, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
            this._vm = registerWindowViewModel;
            this._isSignUpAvailable = true;
        }

        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            if (!this._isSignUpAvailable)
                return;

            this._isSignUpAvailable = false;

            this._vm.SetVisibilities(Visibility.Visible, Visibility.Collapsed, true);

            var dictionary = App.Current.Resources;

            try
            {
                var register = (Register)parameter;

                var response = await this.ExecuteAsync(register);

                if (response.Result.IsSuccessStatusCode)
                {
                    var vm = new CodeConfirmationViewModel(register.Username);
                    var window = new CodeConfirmation(vm);
                    window.Show();
                }
                else
                {
                    RecipeMessageBox.Show((string)dictionary["register_fail"]);
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show((string)dictionary["server_error"]);
            }
            finally
            {
                this._isSignUpAvailable = true;
                this._vm.SetVisibilities(Visibility.Collapsed, Visibility.Visible, false);
            }
        }
    }
}