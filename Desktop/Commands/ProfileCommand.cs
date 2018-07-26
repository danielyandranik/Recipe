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
        /// Creates new instance of <see cref="ProfileCommand<T>"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public ProfileCommand(Func<T, Task<Response<string>>> executeMethod, Func<T, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        { }

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
