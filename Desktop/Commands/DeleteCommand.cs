using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserManagementConsumer.Client;
using Desktop.Views.Windows;
using Desktop.Services;

namespace Desktop.Commands
{
    public class DeleteCommand : ICommand
    {
        private readonly UserManagementApiClient _client;

        public event EventHandler CanExecuteChanged;

        public DeleteCommand()
        {
            var app = (App)App.Current;

            this._client = app.UserApiClient;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var manager = ((App)App.Current).ProfilesMenuManager;

            var response = await this._client.DeleteCurrentProfile(User.Default.CurrentProfile);

            if(response.Status == Status.Error)
            {
                RecipeMessageBox.Show("Error occured while deleting profile");
                return;
            }

            manager.DeleteProfile(User.Default.CurrentProfile);

            RecipeMessageBox.Show("Profile is deleted");
        }
    }
}
