using Desktop.ViewModels;
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
        public HospitalsViewModel HospitalsViewModel { get; private set; }

        public Hospitals()
        {
            this.HospitalsViewModel = new HospitalsViewModel();
            this.DataContext = this.HospitalsViewModel; ;
            InitializeComponent();
        }

        private void EditHospitalClick(object sender, RoutedEventArgs e)
        {
            var id = (int)(sender as Button).Tag;
            this.HospitalsViewModel.EditableHospital = new Institution() { Id = id };
            this.EditPopup.IsOpen = true;
            Application.Current.MainWindow.IsEnabled = false;
        }
        
        private void CloseEditHospitalPopup_Click(object sender, RoutedEventArgs e)
        {
            this.EditPopup.IsOpen = false;
            Application.Current.MainWindow.IsEnabled = true;
        }
    }
}
