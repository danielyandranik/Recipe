using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Desktop.ViewModels;
using Desktop.Views.Pages;
using Desktop.Services;
using Desktop.Commands;

namespace Desktop.Views.Windows
{
    public partial class MainWindow : Window
    {
        private  Medicines _medicines;

        private  Hospitals _hospitals;

        private  Pharmacies _pharmacies;

        private Recipes _recipes;

        private  AddPatientProfile _addPatientProfile;

        private  AddDoctorProfile _addDoctorProfile;

        private  AddPharmacistProfile _addPharmacistProfile;

        private  AddHospitalAdministartorProfile _addHospitalAdministartorProfile;

        private  AddPharmacyAdminProfile _addPharmacyAdminProfile;

        private HospitalAdminApprovals _hospitalAdminApprovals;

        private readonly NavigateService _navigationService;

        private readonly ProfilesMenuManager _profilesMenuManager;

        private  int menuButtonRotateAngle;

        private readonly MainWindowViewModel _mainWindowVM;

        public MainWindowViewModel Vm => this._mainWindowVM;

        public MainWindow()
        {
            // initializing components
            InitializeComponent();

            // setting fields
            this._mainWindowVM = new MainWindowViewModel(this);
            this.DataContext = this._mainWindowVM;
            this._navigationService = new NavigateService(this.frame);
            this.UpdateButtonsVisibilities();
           // this._medicines = new Medicines();
        }

        private async void Medicines_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(ref this._medicines);
            var loadMedicinesService = new LoadMedicinesService(this._medicines.MedicinesViewModel);
            await loadMedicinesService.Load();
        }

        private void Hospitals_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(ref this._hospitals);
        }

        private void Pharmacies_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(ref this._pharmacies);
        }

        private void AddPatientProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._addPatientProfile);
        }

        private void AddDoctorProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._addDoctorProfile);
        }

        private void AddPharmacistProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._addPharmacistProfile);
        }

        private void AddHospitalAdministartortProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._addHospitalAdministartorProfile);
        }

        private void Toggle_menu(object sender, MouseButtonEventArgs e)
        {
            this.menu_opener.LayoutTransform = new RotateTransform(menuButtonRotateAngle);

            menuButtonRotateAngle = (menuButtonRotateAngle == 180) ? 0 : 180;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.frame.NavigationService.Navigate(new Recipes());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.frame.NavigationService.Navigate(new CreateRecipe());
        }

        private void AddPharmacyAdminProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._addPharmacyAdminProfile);
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.menu_opener.LayoutTransform = new RotateTransform(180);
        }

        private async void Main_Loaded(object sender, RoutedEventArgs e)
        {            
            await this._mainWindowVM.LoadService.Execute();
        }

        private async void HospitalAdminApprovalsButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._hospitalAdminApprovals);
            var loadHospitalApprovalsService = new LoadHospitalAdminApprovals(this._hospitalAdminApprovals.ViewModel);
            await loadHospitalApprovalsService.Load();
        }

        private async void RecipesButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._recipes);
            var loadRecipesService = new LoadRecipesService(this._recipes.ViewModel);
            await loadRecipesService.Load();
        }

        public void UpdateButtonsVisibilities()
        {
            this.CreaterecipeButton.Visibility = (User.Default.CurrentProfile == "doctor") ? Visibility.Visible : Visibility.Collapsed;
            this.SellMedicinesButton.Visibility = (User.Default.CurrentProfile == "pharmacist") ? Visibility.Visible : Visibility.Collapsed;
            this.AddMedicineButton.Visibility = (User.Default.CurrentProfile == "ministry_worker") ? Visibility.Visible : Visibility.Collapsed;
            this.AddInstituitionButton.Visibility = (User.Default.CurrentProfile == "ministry_worker") ? Visibility.Visible : Visibility.Collapsed;
            this.MyApprovalsButton.Visibility = (User.Default.CurrentProfile == "doctor" || User.Default.CurrentProfile == "doctor") ? Visibility.Visible : Visibility.Collapsed;
            this.MyRecipesButton.Visibility = (User.Default.CurrentProfile == "patient") ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}