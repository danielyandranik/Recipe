using System.Threading.Tasks;
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
        public PharmaciesViewModel PharmaciesViewModel { get; private set; }

        private LoadPharmMedicinesService service;

        public Pharmacies()
        {
            InitializeComponent();

            this.PharmaciesViewModel = new PharmaciesViewModel();
            this.DataContext = this.PharmaciesViewModel;

            this.service = new LoadPharmMedicinesService(this.PharmaciesViewModel);
        }

        private void EditPharmacyClick(object sender, RoutedEventArgs e)
        {
            var id = (int)(sender as Button).Tag;
            this.PharmaciesViewModel.EditablePharmacy = new Institution() { Id = id };
            this.EditPopup.IsOpen = true;
            Application.Current.MainWindow.IsEnabled = false;
        }

        private void ViewMedicinesClick(object sender, RoutedEventArgs e)
        {
            var id = (int)(sender as Button).Tag;

            this.service.id = id;
            Task response =  this.service.Load();

            this.MedicinesPopup.IsOpen = true;
            Application.Current.MainWindow.IsEnabled = false;
        }

        private void CloseEditPharmacyPopup_Click(object sender, RoutedEventArgs e)
        {
            this.EditPopup.IsOpen = false;
            Application.Current.MainWindow.IsEnabled = true;
        }

        private void CloseViewMedicinesPopup_Click(object sender, RoutedEventArgs e)
        {
            this.MedicinesPopup.IsOpen = false;
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
