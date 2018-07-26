using Desktop.ViewModels;
using Desktop.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace Desktop.Views.Windows

{
    public partial class MainWindow : Window
    {
        private readonly static Medicines medicines;
        private static readonly Hospitals hospitals;
        private static readonly Pharmacies pharmacies;

        private readonly AddPatientProfile addPatientProfile;
        private readonly AddDoctorProfile addDoctorProfile;
        private readonly AddPharmacistProfile addPharmacistProfile;
        private readonly AddHospitalAdministartorProfile addHospitalAdministartorProfile;
        private readonly AddPharmacyAdminProfile addPharmacyAdminProfile;
        private static int menuButtonRotateAngle;

        private readonly MainWindowViewModel _mainWindowVM;

        static MainWindow()
        {
            medicines = new Medicines();
            hospitals = new Hospitals();
            pharmacies = new Pharmacies();
            menuButtonRotateAngle = 180;
        }

        public MainWindow(MainWindowViewModel mainWindowVM)
        {
            InitializeComponent();
           // this.addPatientProfile = new AddPatientProfile();
            this.addDoctorProfile = new AddDoctorProfile();
            this.addPharmacistProfile = new AddPharmacistProfile();
            this.addHospitalAdministartorProfile = new AddHospitalAdministartorProfile();
            this.addPharmacyAdminProfile = new AddPharmacyAdminProfile();
            //this.SourceInitialized += Window_SourceInitialized;

            this._mainWindowVM = mainWindowVM;
            this.DataContext = this._mainWindowVM;
        }

        private void Medicines_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.frame.NavigationService.Navigate(medicines);
        }

        private void Hospitals_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.frame.NavigationService.Navigate(hospitals);
        }

        private void Pharmacies_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.frame.NavigationService.Navigate(pharmacies);
        }

        private void Toggle_menu(object sender, MouseButtonEventArgs e)
        {
            var button = (Image)sender;

            button.LayoutTransform = new RotateTransform(menuButtonRotateAngle);
            menuButtonRotateAngle = (menuButtonRotateAngle == 180) ? 0 : 180;
            this.menu.IsOpen = !this.menu.IsOpen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.AddProfile.IsOpen = !this.AddProfile.IsOpen;
        }

        private void AddPatientProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.NavigationService.Navigate(this.addPatientProfile);
        }

        private void AddDoctorProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.NavigationService.Navigate(this.addDoctorProfile);
        }

        private void AddPharmacistProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.NavigationService.Navigate(this.addPharmacistProfile);
        }

        private void AddHospitalAdministartortProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.NavigationService.Navigate(this.addHospitalAdministartorProfile);
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
            this.frame.NavigationService.Navigate(this.addPharmacyAdminProfile);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            App.Current.Shutdown();
        }

        private void Main_LocationChanged(object sender, EventArgs e)
        {
            this.ResetPopUp();
        }

        private void Main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ResetPopUp();
        }

        private void ResetPopUp()
        {
            var offset = this.menu.HorizontalOffset;
            this.menu.HorizontalOffset = offset + 1;
            this.menu.HorizontalOffset = offset;
        }
    }
}