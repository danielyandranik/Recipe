using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using Desktop.Views.Windows;
using UserManagementConsumer.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Command for verification code confirmation
    /// </summary>
    public class ConfirmCommand : AsyncCommand<UserVerificationInfo, Response<HttpResponseMessage>>
    {
        /// <summary>
        /// Creates new instance of <see cref="ConfirmCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public ConfirmCommand(Func<UserVerificationInfo, Task<Response<HttpResponseMessage>>> executeMethod,
            Func<UserVerificationInfo, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        { }

        /// <summary>
        /// Executes command asynchronously
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            try
            {
                var response = await this.ExecuteAsync((UserVerificationInfo)parameter);

                if (response.Result.StatusCode == HttpStatusCode.BadRequest)
                    RecipeMessageBox.Show("Invalid verification code");
                else if (response.Result.StatusCode == HttpStatusCode.InternalServerError)
                    RecipeMessageBox.Show("Something went wrong");
                else this.ManageWindows();
            }
            catch
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }

        /// <summary>
        /// Manages windows
        /// </summary>
        private void ManageWindows()
        {
            ((App)App.Current).SignInWindow.Show();

            for (var counter = App.Current.Windows.Count - 2; counter >= 0; counter--)
            {
                App.Current.Windows[counter].Close();
            }
        }
    }
}
