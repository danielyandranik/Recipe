using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Controls;
using Desktop.Services;
using Desktop.Views.Windows;
using Desktop.ViewModels;
using Desktop.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Load service
    /// </summary>
    public class LoadService 
    {
        /// <summary>
        /// User info loader
        /// </summary>
        private readonly UserInfoLoader _userInfoLoader;

        /// <summary>
        /// Menu item
        /// </summary>
        private readonly MenuItem _menuItem;

        /// <summary>
        /// Profiles menu manager
        /// </summary>
        private readonly ProfilesMenuManager _profilesMenuManager;

        /// <summary>
        /// Main window viewmode
        /// </summary>
        private readonly MainWindowViewModel _vm;

        /// <summary>
        /// Creates new instance of <see cref="LoadService"/>
        /// </summary>
        /// <param name="menuItem">Menu item</param>
        /// <param name="vm">Viewmodel</param>
        public LoadService(MenuItem menuItem, MainWindowViewModel vm)
        {
            // setting fields
            this._menuItem = menuItem;
            this._userInfoLoader = new UserInfoLoader();
            this._profilesMenuManager = new ProfilesMenuManager(this._menuItem, vm);
            this._vm = vm;
        }

        /// <summary>
        /// Executes service operation.
        /// </summary>
        /// <returns>nothing</returns>
        public async Task Execute()
        {
            ((App)App.Current).ProfilesMenuManager = this._profilesMenuManager;

            var load = await this._userInfoLoader.Execute();

            if(load == null)
            {
                RecipeMessageBox.Show("Unable to load");
                return;
            }

            this.InitializeVM(load);
            this.SaveSettings(load);

            this._profilesMenuManager.AddProfiles(load.Profiles);

            if (load.CurrentProfile == "none")
            {
                this._profilesMenuManager.CollapseAll();
                return;
            }

            this._profilesMenuManager.UpdateButtonsVisibilities();
            
        }

        /// <summary>
        /// Initializes viewmodel
        /// </summary>
        /// <param name="load">user's loaded information</param>
        private void InitializeVM(UserInitialInfo load)
        {
            // setting viewmodel properties
            this._vm.CurrentProfile = this._profilesMenuManager.ApiToUi[load.CurrentProfile];
            this._vm.FullName = load.FullName;
            this._vm.Username = load.Username;
            this._vm.PhotoUrl = ConfigurationManager.AppSettings[load.CurrentProfile];
        }

        /// <summary>
        /// Saves user's default settings
        /// </summary>
        /// <param name="load">user's loaded information</param>
        private void SaveSettings(UserInitialInfo load)
        {
            User.Default.CurrentProfile = load.CurrentProfile;
            User.Default.Id = load.Id;
            User.Default.Save();
        }
    }
}
