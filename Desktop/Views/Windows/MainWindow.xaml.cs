using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Controls;
using Desktop.ViewModels;
using Desktop.Views.Pages;
using Desktop.Services;

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

        private AddInstitution _addInstitution;

        private SellMedicines _sellMedicines;

        private CreateRecipe _createRecipe;

        private AddMedicine _addMedicine;

        private HospitalAdminApprovals _hospitalAdminApprovals;

        private readonly NavigateService _navigationService;

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
        }

        private async void Medicines_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(ref this._medicines);
            var loadMedicinesService = new LoadMedicinesService(this._medicines.MedicinesViewModel);
            await loadMedicinesService.Load();
        }

        private async void Hospitals_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(ref this._hospitals);
            var loadHospitalsService = new LoadHospitalsService(this._hospitals.HospitalsViewModel);
            await loadHospitalsService.Load();
        }

        private async void Pharmacies_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(ref this._pharmacies);
            var loadPharmaciesService = new LoadPharmaciesService(this._pharmacies.PharmaciesViewModel);
            await loadPharmaciesService.Load();
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

        private void AddInstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._addInstitution);
        }

        private void AddHospitalAdminProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._addHospitalAdministartorProfile);
        }

        private void SellMedicinesButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._sellMedicines);
        }

        private async void CreateRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._createRecipe);
            var loadMedicinesService = new LoadMedicinesService(this._createRecipe.ViewModel);
            await loadMedicinesService.Load();
        }

        private void AddMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._addMedicine);
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

            var mapPage = ((App)App.Current).MapPage;

            this.frame.Navigate(mapPage);
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

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var mapPage = ((App)App.Current).MapPage;

            this.frame.Navigate(mapPage);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            var menu = this.profiles.Items;

            var manager = ((App)App.Current).ProfilesMenuManager;

            foreach(var item in menu)
            {
                var menuItem = (MenuItem)item;

                menuItem.Click -= manager.ChangeProfileEventHandler;
            }

            var app = (App)App.Current;

            app.ProfilesMenuManager = null;
        }
    }
}