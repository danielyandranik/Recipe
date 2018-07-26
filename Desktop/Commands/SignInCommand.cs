using System;
using AuthTokenService;
using Desktop.Models;
using Desktop.Services;
using Desktop.Views.Windows;
using UserManagementConsumer.Client;

namespace Desktop.Commands
{
    public class SignInCommand : CommandBase
    {
        private readonly TokenProvider _tokenProvider;

        private readonly UserManagementApiClient _client;

        private readonly HyperLinkService _hyperLinkService;

        public SignInCommand()
        {
            var app = ((App)App.Current);
            this._tokenProvider = app.TokenProvider;
            this._client = app.UserApiClient;
            this._hyperLinkService = new HyperLinkService();
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter == null)
                return true;

            var signInInfo = (SignInInfo)parameter;

            if (string.IsNullOrEmpty(signInInfo.Username) || string.IsNullOrEmpty(signInInfo.Password))
                return false;

            return signInInfo.Password.Length > 7;
        }

        public override async void Execute(object parameter)
        {
            var signInInfo = (SignInInfo)parameter;

            try
            {
                var status = await this._tokenProvider.SignInAsync(signInInfo.Username, signInInfo.Password);

                if(status == TokenStatus.Error)
                {
                    RecipeMessageBox.Show("Invalid username or password");
                    return;
                }

                var response = await this._client.GetUserByUsernameAsync(signInInfo.Username);

                if(response.Status == Status.Error)
                {
                    RecipeMessageBox.Show("Error occured");
                    return;
                }

                var user = response.Result;

                User.Default.Id = user.Id;
                User.Default.Username = user.Username;
                User.Default.Save();

                this._hyperLinkService.Navigate<SignIn, MainWindow>();
            }
            catch (Exception)
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }
    }
}
