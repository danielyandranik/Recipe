using System;
using AuthTokenService;
using Desktop.Models;
using Desktop.Services;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    /// <summary>
    /// Sign In Command
    /// </summary>
    public class SignInCommand : CommandBase
    {
        /// <summary>
        /// Token provider
        /// </summary>
        private readonly TokenProvider _tokenProvider;

        /// <summary>
        /// User info loader
        /// </summary>
        private readonly UserInfoLoader _userInfoLoader;

        /// <summary>
        /// Hyper link service
        /// </summary>
        private readonly HyperLinkService _hyperLinkService;

        /// <summary>
        /// Creates new instance of <see cref="SignInCommand"/>
        /// </summary>
        public SignInCommand()
        {
            // getting current app
            var app = ((App)App.Current);

            // setting fields
            this._tokenProvider = app.TokenProvider;
            this._userInfoLoader = new UserInfoLoader();
            this._hyperLinkService = new HyperLinkService();
        }

        /// <summary>
        /// Determines if the command can be executes.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating whether the command can be executed.</returns>
        public override bool CanExecute(object parameter)
        {
            if (parameter == null)
                return true;

            var signInInfo = (SignInInfo)parameter;

            if (string.IsNullOrEmpty(signInInfo.Username) || string.IsNullOrEmpty(signInInfo.Password))
                return false;

            return signInInfo.Password.Length > 7;
        }

        /// <summary>
        /// Executes the command operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            var signInInfo = (SignInInfo)parameter;

            try
            {
                var status = await this._tokenProvider.SignInAsync(signInInfo.Username, signInInfo.Password);

                if (status == TokenStatus.Error)
                {
                    RecipeMessageBox.Show("Invalid username or password");
                    return;
                }

                User.Default.Username = signInInfo.Username;
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
