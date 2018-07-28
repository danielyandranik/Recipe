using System.Windows.Input;
using Desktop.Services;
using Desktop.Views.Windows;
using AuthTokenService;
using System;

namespace Desktop.Commands
{
    public class SignOutCommand : ICommand
    {
        private readonly TokenProvider _tokenProvider;

        private readonly HyperLinkService _hyperlinkService;

        public event EventHandler CanExecuteChanged;

        public SignOutCommand()
        {
            this._tokenProvider = ((App)App.Current).TokenProvider;
            this._hyperlinkService = new HyperLinkService();
        }

        public bool CanExecute(object parameter) => true;

        public  async void Execute(object parameter)
        {
            var response = await this._tokenProvider.SignOutAsync();

            if(response == TokenStatus.Error)
            {
                RecipeMessageBox.Show("Error occured while signing out.");
                return;
            }

            this._hyperlinkService.Navigate<MainWindow, SignIn>();            
        }
    }
}
