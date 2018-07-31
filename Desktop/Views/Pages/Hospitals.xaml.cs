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
            var hospital = (Institution)(sender as Button).Tag;
            ((HospitalsViewModel)this.DataContext).EditableHospital = hospital;
        }
    }
}
