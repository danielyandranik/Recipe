using InstitutionClient.Models;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for Hospitals.xaml
    /// </summary>
    public partial class Hospitals : Page
    {
        public Hospitals()
        {
            InitializeComponent();

            var hospitalsViewModel = new HospitalsViewModel();
            this.DataContext = hospitalsViewModel;
        }

        private void EditPharmacyClick(object sender, RoutedEventArgs e)
        {
            var hospital = (Institution)(sender as Button).Tag;
            ((HospitalsViewModel)this.DataContext).EditableHospital = hospital;
        }
    }
}
