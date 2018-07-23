using Desktop.ViewModels;
using Desktop.Views;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Commands
{
    public class ConfirmCommand : CommandBase
    {
        private ConfirmationViewModel _confirmationViewModel;

        private UserManagementApiClient _userManagementApiClient;

        public ConfirmCommand(ConfirmationViewModel confirmationViewModel)
        {
            this._confirmationViewModel = confirmationViewModel;
            this._userManagementApiClient = new UserManagementApiClient();
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;

            return ((string)parameter).Length == 32;
        }

        public override async void ExecuteAsync(object parameter)
        {
            var code = (string)parameter;

            try
            {
                var response = await this._userManagementApiClient.VerifyAsync(
                    new UserVerificationInfo
                    {
                        Username = this._confirmationViewModel.Username,
                        VerifyKey = code
                    });

                if (response.Result.IsSuccessStatusCode)
                {
                    new SignIn().Show();

                    for (var count = App.Current.Windows.Count - 2; count >= 0; count--)
                    {
                        App.Current.Windows[count].Close();
                    }
                }                
            }
            catch { }
        }
    }
}
