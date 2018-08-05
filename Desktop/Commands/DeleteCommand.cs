using System;
using System.Windows.Input;
using UserManagementConsumer.Client;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    /// <summary>
    /// Delete Command
    /// </summary>
    public class DeleteCommand : ICommand
    {
        /// <summary>
        /// User management API client
        /// </summary>
        private readonly UserManagementApiClient _client;

        /// <summary>
        /// CanExecuteChanged event
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates new instance of <see cref="DeleteCommand"/>
        /// </summary>
        public DeleteCommand()
        {
            // getting app current
            var app = (App)App.Current;

            // setting User Management API client
            this._client = app.UserApiClient;
        }

        /// <summary>
        /// Determines if the command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating whether the command can be executed.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the command operation.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async void Execute(object parameter)
        {
            var manager = ((App)App.Current).ProfilesMenuManager;

            var response = await this._client.DeleteCurrentProfile(User.Default.CurrentProfile);

            var dictionary = App.Current.Resources;

            if(response.Status == Status.Error)
            {
                RecipeMessageBox.Show((string)dictionary["profile_del_error"]);
                return;
            }

            manager.DeleteProfile(User.Default.CurrentProfile);

            RecipeMessageBox.Show((string)dictionary["profile_del_success"]);
        }
    }
}
