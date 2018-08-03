using System;
using System.Net.Http;
using System.Threading.Tasks;
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
        /// Creates new instance of <see cref="RegisterCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public RegisterCommand(Func<Register, Task<Response<HttpResponseMessage>>> executeMethod, Func<Register, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        { }

        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
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
                    RecipeMessageBox.Show("Unable to register");
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }
    }
}