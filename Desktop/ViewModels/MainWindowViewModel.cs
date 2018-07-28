using System.Collections.Generic;
using Desktop.Models;
using Desktop.Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _username;

        private string _currentProfile;

        private string _fullName;

        private IEnumerable<string> _profiles;

        private readonly RelayCommand _popupReplace;

        private readonly SignOutCommand _signOutCommand;

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

        public RelayCommand PopupReplaceCommand => this._popupReplace;

        public SignOutCommand SignOutCommand => this._signOutCommand;

        public MainWindowViewModel(UserInitialInfo user)
        {
            this._username = user.Username;
            this._fullName = user.FullName;
            this._currentProfile = user.CurrentProfile;
            this._profiles = user.Profiles;
            this._signOutCommand = new SignOutCommand();
        }
    }
}
