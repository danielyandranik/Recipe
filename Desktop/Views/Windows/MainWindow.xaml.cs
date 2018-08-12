using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Controls;
using Desktop.ViewModels;
using Desktop.Views.Pages;
using Desktop.Services;
using System;
using System.Windows.Navigation;

namespace Desktop.Views.Windows
{
    public partial class MainWindow : Window
    {
        private  Medicines _medicines;

        private Recipes _recipes;

        private SellMedicines _sellMedicines;

        private CreateRecipe _createRecipe;

        private HospitalAdminApprovals _hospitalAdminApprovals;

        private PharmacyAdminApprovals _pharmacyAdminApprovals;

        private MinistryWorkerApprovals _ministryWorkerApprovals;

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

            this.frame.Navigated += this.Navigate;
        }

        private void Navigate(object sender, NavigationEventArgs e)
        {
            var qrDecoder = ((App)App.Current).QrDecoderService;

            var pageType = e.Content.GetType();

            if (pageType == typeof(SellMedicines))
                qrDecoder?.Start();
            else
                qrDecoder?.Stop();
        }

        private async void Medicines_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(ref this._medicines);
            var loadMedicinesService = new LoadMedicinesService(this._medicines.MedicinesViewModel);
            await loadMedicinesService.Load();
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
            var menu = this.profiles.Items;

            var manager = ((App)App.Current).ProfilesMenuManager;

            foreach(var item in menu)
            {
                var menuItem = (MenuItem)item;

                menuItem.Click -= manager.ChangeProfileEventHandler;              
            }

            var app = (App)App.Current;

            app.ProfilesMenuManager = null;

            app.QrDecoderService?.Stop();

            base.OnClosing(e);
        }

        private async void MyApprovalsButton_Click(object sender, RoutedEventArgs e)
        {
            if(User.Default.CurrentProfile == "hospital_director")
            {
                this._navigationService.Navigate(ref this._hospitalAdminApprovals);

                var vm = (HospitalAdminApprovalViewModel)this._hospitalAdminApprovals.DataContext;

                var service = new LoadHospitalAdminApprovals(vm);

                await service.Load();
            }
            else if(User.Default.CurrentProfile == "pharmacy_admin")
            {
                this._navigationService.Navigate(ref this._pharmacyAdminApprovals);

                var vm = (PharmacyAdminApprovalsViewModel)this._pharmacyAdminApprovals.DataContext;

                var service = new LoadPharmacyAdminApprovalsService(vm);

                await service.Load();
            }
            else
            {
                this._navigationService.Navigate(ref this._ministryWorkerApprovals);

                var vm = (MinistryWorkerApprovalsViewModel)this._ministryWorkerApprovals.DataContext;

                var service = new LoadMinistryWorkerApprovals(vm);

                await service.Load();
            }
        }
    }
}