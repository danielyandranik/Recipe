using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using UserManagementConsumer.Client;
using Desktop.ViewModels;
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
        /// Code confirmation view model
        /// </summary>
        private readonly CodeConfirmationViewModel _vm;

        /// <summary>
        /// Is confirm available
        /// </summary>
        private bool _isConfirmAvailable;

        /// <summary>
        /// Creates new instance of <see cref="ConfirmCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public ConfirmCommand(CodeConfirmationViewModel codeConfirmationViewModel,
            Func<UserVerificationInfo, Task<Response<HttpResponseMessage>>> executeMethod,
            Func<UserVerificationInfo, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
            this._vm = codeConfirmationViewModel;
            this._isConfirmAvailable = true;
        }

        /// <summary>
        /// Executes command asynchronously
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            if (!this._isConfirmAvailable)
                return;

            this._isConfirmAvailable = false;
            this._vm.SetVisibilities(Visibility.Visible, Visibility.Collapsed, true);

            var dictionary = App.Current.Resources;

            try
            {
                var response = await this.ExecuteAsync((UserVerificationInfo)parameter);

                if (response.Result.StatusCode == HttpStatusCode.BadRequest)
                    RecipeMessageBox.Show((string)dictionary["ver_code_fail"]);
                else if (response.Result.StatusCode == HttpStatusCode.InternalServerError)
                    RecipeMessageBox.Show((string)dictionary["ver_server_error"]);
                else this.ManageWindows();
            }
            catch
            {
                RecipeMessageBox.Show((string)dictionary["server_error"]);
            }
            finally
            {
                this._isConfirmAvailable = true;
                this._vm.SetVisibilities(Visibility.Collapsed, Visibility.Visible, false);
            }
        }

        /// <summary>
        /// Manages windows
        /// </summary>
        private void ManageWindows()
        {
            new SignIn().Show();

            for (var counter = App.Current.Windows.Count - 2; counter >= 0; counter--)
            {
                App.Current.Windows[counter].Close();
            }
        }
    }
}
