using System.Collections.Generic;
using Desktop.Models;
using Desktop.Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Desktop.Services;
using Desktop.Views.Windows;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _username;

        private string _currentProfile;

        private string _fullName;

        private IEnumerable<string> _profiles;

        private readonly HyperLinkService _hyperLinkService;

        private readonly MainWindow _mainWindow;

        private readonly RelayCommand _hyperLinkCommand;

        private readonly SignOutCommand _signOutCommand;

        private readonly LoadService _loadService;

        public string Username
        {
            get => this._username;

            set => this.Set("Username", ref this._username, value);
        }

        public string CurrentProfile
        {
            get => this._currentProfile;

            set => this.Set("CurrentProfile",ref this._currentProfile,value);
        }

        public string FullName
        {
            get => this._fullName;

            set => this.Set("FullName", ref this._fullName, value);
        }

        public IEnumerable<string> Profiles
        {
            get => this._profiles;

            set => this.Set("Profiles", ref this._profiles, value);
        }

        public RelayCommand HyperLinkCommand => this._hyperLinkCommand;

        public SignOutCommand SignOutCommand => this._signOutCommand;

        public LoadService LoadService => this._loadService;

        public MainWindowViewModel(MainWindow mainWindow)
        {           
            this._signOutCommand = new SignOutCommand();
            this._hyperLinkService = new HyperLinkService();
            this._hyperLinkCommand = new RelayCommand(this._hyperLinkService.Navigate<MainWindow,RegisterWindow>,() => true);
            this._mainWindow = mainWindow;
            this._loadService = new LoadService(this._mainWindow.profiles, this);            
        }
    }
}
