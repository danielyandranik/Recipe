using System;
using System.Windows.Input;
using AuthTokenService;
using Desktop.Services;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    /// <summary>
    /// Sign Out command
    /// </summary>
    public class SignOutCommand : ICommand
    {
        /// <summary>
        /// Token provider
        /// </summary>
        private readonly TokenProvider _tokenProvider;

        /// <summary>
        /// Hyper link service
        /// </summary>
        private readonly HyperLinkService _hyperlinkService;

        /// <summary>
        /// CanExecuteChanged evenet
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates new instance of <see cref="SignOutCommand"/>
        /// </summary>
        public SignOutCommand()
        {
            // setting fields
            this._tokenProvider = ((App)App.Current).TokenProvider;
            this._hyperlinkService = new HyperLinkService();
        }

        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>boolean value indicating whether the command can be executed.</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the command operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public  async void Execute(object parameter)
        {
            var response = await this._tokenProvider.SignOutAsync();

            if(response == TokenStatus.Error)
            {
                RecipeMessageBox.Show("Error occured while signing out.");
                return;
            }

            this.ResetSettings();

            this._hyperlinkService.Navigate<MainWindow, SignIn>();            
        }

        /// <summary>
        /// Resets User default setting
        /// </summary>
        private void ResetSettings()
        {
            User.Default.Id = 0;
            User.Default.CurrentProfile = null;
            User.Default.Username = null;
            User.Default.RefreshToken = null;
            User.Default.Save();
        }
    }
}
