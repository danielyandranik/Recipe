using RecipeClient;
using UserManagementConsumer.Client;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    /// <summary>
    /// QR send command
    /// </summary>
    public class SendQRCommand : CommandBase
    {
        /// <summary>
        /// Recipe API client
        /// </summary>
        private readonly RecipeClient.RecipeClient _recipeClient;

        /// <summary>
        /// User Management API
        /// </summary>
        private readonly UserManagementApiClient _userApiClient;

        /// <summary>
        /// Creates new instance of <see cref="SendQRCommand"/>
        /// </summary>
        public SendQRCommand()
        {
            var app = ((App)App.Current);
            this._recipeClient = app.RecipeClient;
            this._userApiClient = app.UserApiClient;
        }

        /// <summary>
        /// Determines if the command can be executed
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating wheter the command can be executed</returns>
        public override bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            var userResponse = await this._userApiClient.GetUserAsync(User.Default.Id);

            var dictionary = App.Current.Resources;

            if(userResponse.Status == Status.Error)
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

            if(!qrResponse.IsSuccessStatusCode)
            {
                RecipeMessageBox.Show((string)dictionary["qr_send_fail"]);
                return;
            }

            RecipeMessageBox.Show((string)dictionary["qr_send_success"]);
        }
    }
}
