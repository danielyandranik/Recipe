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

        private  AddPatientProfile _addPatientProfile;

        private  AddDoctorProfile _addDoctorProfile;

        private  AddPharmacistProfile _addPharmacistProfile;

        private  AddHospitalAdministartorProfile _addHospitalAdministartorProfile;

        private  AddPharmacyAdminProfile _addPharmacyAdminProfile;

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

           // this._medicines = new Medicines();
        }

        private void Medicines_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(this._medicines);
        }

        private void Hospitals_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(this._hospitals);
        }

        private void Pharmacies_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(this._pharmacies);
        }

        private void AddPatientProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(this._addPatientProfile);
        }

        private void AddDoctorProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(this._addDoctorProfile);
        }

        private void AddPharmacistProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(this._addPharmacistProfile);
        }

        private void AddHospitalAdministartortProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(this._addHospitalAdministartorProfile);
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
            this._navigationService.Navigate(this._addPharmacyAdminProfile);
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.menu_opener.LayoutTransform = new RotateTransform(180);
        }

        private void MedicinesButtonClick(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(this._medicines);
        }

        private void HospitalsButtonClick(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(this._hospitals);
        }

        private void PharmaciesButtonClick(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(this._pharmacies);
        }

        private async void Main_Loaded(object sender, RoutedEventArgs e)
        {            
            await this._mainWindowVM.LoadService.Execute();
        }
    }
}