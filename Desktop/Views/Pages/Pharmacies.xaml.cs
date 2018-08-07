using System.Windows;
using System.Windows.Controls;
using Desktop.Services;
using Desktop.ViewModels;
using InstitutionClient.Models;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for Phamracies.xaml
    /// </summary>
    public partial class Pharmacies : Page
    {
        private readonly NavigateService navigationService;

        private PharmMedicines pharmMedicines;


        public PharmaciesViewModel PharmaciesViewModel { get; private set; }

        public Pharmacies()
        {
            this.navigationService = new NavigateService(this.frame);
            this.PharmaciesViewModel = new PharmaciesViewModel();
            this.DataContext = this.PharmaciesViewModel;
            InitializeComponent();
        }

        private void EditPharmacyClick(object sender, RoutedEventArgs e)
        {
            var id = (int)(sender as Button).Tag;
            this.PharmaciesViewModel.EditablePharmacy = new Institution() { Id = id };
            this.EditPopup.IsOpen = true;
            Application.Current.MainWindow.IsEnabled = false;
        }

        private async void ViewMedicinesClick(object sender, RoutedEventArgs e)
        {
            var id = (int)(sender as Button).Tag;
            this.pharmMedicines.MedicinesViewModel.pharmacyId = id;
            this.navigationService.Navigate(ref this.pharmMedicines);
            var loadPharmMedicinesService = new LoadPharmMedicinesService(this.pharmMedicines.MedicinesViewModel);
            await loadPharmMedicinesService.Load();
        }

        private void CloseEditPharmacyPopup_Click(object sender, RoutedEventArgs e)
        {
            this.EditPopup.IsOpen = false;
            Application.Current.MainWindow.IsEnabled = true;
        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox == this.medicine)
            {
               // await this.PharmaciesViewModel.Filter(pharmacy => pharmacy.Medicine.Contains(textbox.Text));
            }
            else if (textbox == this.name)
            {
                await this.PharmaciesViewModel.Filter(pharmacy => pharmacy.Name.Contains(textbox.Text));
            }
            else
            {
                await this.PharmaciesViewModel.Filter(pharmacy => pharmacy.Address.Contains(textbox.Text));
            }
        }
    }
}
