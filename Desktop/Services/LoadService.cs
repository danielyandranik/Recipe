using System;
using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Services;
using Desktop.Views.Windows;
using Desktop.ViewModels;
using Desktop.Models;
using System.Threading.Tasks;
using System.Configuration;

namespace Desktop.Commands
{
    public class LoadService 
    {
        private readonly UserInfoLoader _userInfoLoader;

        private readonly MenuItem _menuItem;

        private readonly ProfilesMenuManager _profilesMenuManager;

        private readonly MainWindowViewModel _vm;

        public LoadService(MenuItem menuItem, MainWindowViewModel vm)
        {
            this._menuItem = menuItem;
            this._userInfoLoader = new UserInfoLoader();
            this._profilesMenuManager = new ProfilesMenuManager(this._menuItem, vm);
            this._vm = vm;
        }

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

        private void InitializeVM(UserInitialInfo load)
        {
            this._vm.CurrentProfile = this._profilesMenuManager.ApiToUi[load.CurrentProfile];
            this._vm.FullName = load.FullName;
            this._vm.Username = load.Username;
            this._vm.PhotoUrl = ConfigurationManager.AppSettings[load.CurrentProfile];
        }

        private void SaveSettings(UserInitialInfo load)
        {
            User.Default.CurrentProfile = load.CurrentProfile;
            User.Default.Id = load.Id;
            User.Default.Save();
        }
    }
}
