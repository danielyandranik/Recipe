using Desktop.Views.Windows;
using System;
using System.Threading.Tasks;
using UserManagementConsumer.Client;

namespace Desktop.Commands
{
    /// <summary>
    /// Command for adding some type of profile
    /// </summary>
    public class ProfileCommand<T> : AsyncCommand<T, Response<string>> where T : class
    {
        /// <summary>
        /// Name
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Creates new instance of <see cref="ProfileCommand<T>"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public ProfileCommand(Func<T, Task<Response<string>>> executeMethod, Func<T, bool> canExecuteMethod,string name) :
            base(executeMethod, canExecuteMethod)
        {
            this._name = name;
        }

        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            try
            {
                var profileInfo = parameter as T;

                var response = await this.ExecuteAsync(profileInfo);

                if (response.Status == Status.Ok)
                {
                    RecipeMessageBox.Show("Profile is added");

                    var manager = ((App)App.Current).ProfilesMenuManager;

                    manager.AddProfile(this._name);
                }
                else
                {
                    RecipeMessageBox.Show("Unable to add profile");
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }
    }
}
