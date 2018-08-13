using System;
using System.Threading.Tasks;
using System.Windows;
using UserManagementConsumer.Client;
using Desktop.ViewModels;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    /// <summary>
    /// Command for adding some type of profile
    /// </summary>
    public class ProfileCommand<T> : AsyncCommand<T, Response<string>> where T : class
    {
        /// <summary>
        /// Boolean value indicating whether the done button is available
        /// </summary>
        private bool _isDoneAvailable;

        /// <summary>
        /// Loadable page viewmodel
        /// </summary>
        private readonly LoadablePageViewModel _vm;

        /// <summary>
        /// Name
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Creates new instance of <see cref="ProfileCommand<T>"/>
        /// </summary>
        /// <param name="loadablePageViewModel">Loadable page viewmodel</param>
        /// <param name="name">Profile name</param>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public ProfileCommand(LoadablePageViewModel loadablePageViewModel, string name,
            Func<T, Task<Response<string>>> executeMethod, Func<T, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
            this._vm = loadablePageViewModel;
            this._name = name;
            this._isDoneAvailable = true;
        }

        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            if (!this._isDoneAvailable)
                return;

            var dictionary = App.Current.Resources;

            this._isDoneAvailable = false;
            this._vm.SetVisibilities(Visibility.Visible, true);

            try
            {
                var profileInfo = parameter as T;
                
                var response = await this.ExecuteAsync(profileInfo);

                if (response.Status == Status.Ok)
                {
                    RecipeMessageBox.Show((string)dictionary["profile_add_success"]);

                    var manager = ((App)App.Current).ProfilesMenuManager;

                    manager.AddProfile(this._name);
                }
                else
                {
                    RecipeMessageBox.Show((string)dictionary["profile_add_fail"]);
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show((string)dictionary["server_error"]);
            }
            finally
            {
                this._isDoneAvailable = true;
                this._vm.SetVisibilities(Visibility.Collapsed, false);
            }
        }
    }
}
