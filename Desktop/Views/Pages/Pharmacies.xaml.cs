using System.Windows;
using System.Windows.Controls;
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

        public Pharmacies()
        {
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
               // await this.PharmaciesViewModel.Filter(pharmacy => pharmacy.medicine.Contains(textbox.Text));
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
