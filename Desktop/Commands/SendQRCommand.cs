using RecipeClient;
using UserManagementConsumer.Client;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    public class SendQRCommand : CommandBase
    {
        private readonly RecipeClient.RecipeClient _recipeClient;

        private readonly UserManagementApiClient _userApiClient;

        public SendQRCommand()
        {
            var app = ((App)App.Current);
            this._recipeClient = app.RecipeClient;
            this._userApiClient = app.UserApiClient;
        }

        public override bool CanExecute(object parameter)
        {
            return parameter != null;
        }

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
