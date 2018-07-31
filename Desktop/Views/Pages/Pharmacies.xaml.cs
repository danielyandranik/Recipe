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
            var pharmacy = (Institution)(sender as Button).Tag;
            ((PharmaciesViewModel)this.DataContext).EditablePharmacy = pharmacy;
        }
    }
}
