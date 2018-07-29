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
        public Pharmacies()
        {
            InitializeComponent();

            var pharmaciesViewModel = new HospitalsViewModel();
            this.DataContext = pharmaciesViewModel;
        }
        
        private void EditPharmacyClick(object sender, RoutedEventArgs e)
        {
            var pharmacy = (Institution)(sender as Button).Tag;
            ((HospitalsViewModel)this.DataContext).EditablePharmacy = pharmacy;
        }
    }
}
