using System.Collections.Generic;
using Desktop.Models;
using Desktop.Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Desktop.Services;
using Desktop.Views.Windows;
using System.Windows.Input;
using System.Windows;

namespace Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _username;

        private string _currentProfile;

        private string _fullName;

        private string _photoUrl;

        private Visibility _myRecipesVisibility;

        private Visibility _createRecipeVisibility;

        private Visibility _myApprovalsVisibility;

        private Visibility _addMedicineVisibility;

        private Visibility _addInstitutionVisibility;

        private Visibility _sellMedicinesVisibility;

        private Visibility _deleteVisibility;

        private readonly HyperLinkService _hyperLinkService;

        private readonly MainWindow _mainWindow;

        private readonly SignOutCommand _signOutCommand;

        private readonly LoadService _loadService;

        private readonly DeleteCommand _deleteCommand;

        private readonly ChangeLangCommand _changeLangCommand;

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

        public Visibility MyRecipesVisibility
        {
            get => this._myRecipesVisibility;

            set => this.Set("MyRecipesVisibility", ref this._myRecipesVisibility, value);
        }

        public Visibility CreateRecipeVisibility
        {
            get => this._createRecipeVisibility;

            set => this.Set("CreateRecipeVisibility", ref this._createRecipeVisibility, value);
        }

        public Visibility MyApprovalsVisibility
        {
            get => this._myApprovalsVisibility;

            set => this.Set("MyApprovalsVisibility", ref this._myApprovalsVisibility, value);
        }

        public Visibility AddInstitutionVisibility
        {
            get => this._addInstitutionVisibility;

            set => this.Set("AddInstitutionVisibility", ref this._addInstitutionVisibility, value);
        }

        public Visibility AddMedicineVisibility
        {
            get => this._addMedicineVisibility;

            set => this.Set("AddMedicineVisibility", ref this._addMedicineVisibility, value);
        }

        public Visibility SellMedicinesVisibility
        {
            get => this._sellMedicinesVisibility;

            set => this.Set("SellMedicinesVisibility", ref this._sellMedicinesVisibility, value);
        }

        public Visibility DeleteVisibility
        {
            get => this._deleteVisibility;

            set => this.Set("DeleteVisibility", ref this._deleteVisibility, value);
        }

        public string PhotoUrl
        {
            get => this._photoUrl;

            set => this.Set("PhotoUrl", ref this._photoUrl, value);
        }

        public SignOutCommand SignOutCommand => this._signOutCommand;

        public LoadService LoadService => this._loadService;

        public DeleteCommand DeleteCommand => this._deleteCommand;

        public ChangeLangCommand ChangeLangCommand => this._changeLangCommand;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this._signOutCommand = new SignOutCommand();
            this._hyperLinkService = new HyperLinkService();
            this._mainWindow = mainWindow;
            this._loadService = new LoadService(this._mainWindow.profiles, this);
            this._deleteCommand = new DeleteCommand();
            this._changeLangCommand = new ChangeLangCommand();
        }
    }
}
