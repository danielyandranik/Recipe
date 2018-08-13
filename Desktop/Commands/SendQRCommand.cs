using System.Windows;
using RecipeClient;
using UserManagementConsumer.Client;
using Desktop.Views.Windows;
using Desktop.ViewModels;

namespace Desktop.Commands
{
    /// <summary>
    /// QR send command
    /// </summary>
    public class SendQRCommand : CommandBase
    {
        /// <summary>
        /// Boolean value indicating wheter the Send button is available.
        /// </summary>
        private bool _isSendAvailable;

        /// <summary>
        /// Recipe API client
        /// </summary>
        private readonly RecipeClient.RecipeClient _recipeClient;

        /// <summary>
        /// User Management API
        /// </summary>
        private readonly UserManagementApiClient _userApiClient;

        /// <summary>
        /// Recipes page viewmodel
        /// </summary>
        private readonly RecipesViewModel _vm;

        /// <summary>
        /// Creates new instance of <see cref="SendQRCommand"/>
        /// </summary>
        public SendQRCommand(RecipesViewModel recipesViewModel)
        {
            var app = ((App)App.Current);
            this._recipeClient = app.RecipeClient;
            this._userApiClient = app.UserApiClient;

            this._vm = recipesViewModel;
            this._isSendAvailable = true;
        }

        /// <summary>
        /// Determines if the command can be executed
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating wheter the command can be executed</returns>
        public override bool CanExecute(object parameter)
        {
            return parameter != null && this._isSendAvailable;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            this._isSendAvailable = false;
            this._vm.SetVisibilities(Visibility.Visible, true);

            var dictionary = App.Current.Resources;

            try
            {

                var userResponse = await this._userApiClient.GetUserAsync(User.Default.Id);

                if (userResponse.Status == Status.Error)
                {
                    RecipeMessageBox.Show((string)dictionary["mail_get_fail"]);
                    return;
                }

                var email = userResponse.Result.Email;

                var recipeId = (string)parameter;

                var qrSendInfo = new QrSendInfo
                {
                    Email = email,
                    RecipeId = recipeId
                };

                var qrResponse = await this._recipeClient.SendQrReqeust(qrSendInfo);

                if (!qrResponse.IsSuccessStatusCode)
                {
                    RecipeMessageBox.Show((string)dictionary["qr_send_fail"]);
                    return;
                }

                RecipeMessageBox.Show((string)dictionary["qr_send_success"]);
            }
            catch
            {
                RecipeMessageBox.Show((string)dictionary["qr_send_error"]);
            }
            finally
            {
                this._isSendAvailable = true;
                this._vm.SetVisibilities(Visibility.Collapsed, false);
            }
        }
    }
}
