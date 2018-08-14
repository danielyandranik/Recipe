using Desktop.ViewModels;
using MedicineApiClient;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for Medicines.xaml
    /// </summary>
    public partial class Medicines : Page
    {
        public MedicinesViewModel MedicinesViewModel { get; private set; }

        public Medicines()
        {
            InitializeComponent();

            this.MedicinesViewModel = new MedicinesViewModel();
            this.DataContext = this.MedicinesViewModel;
        }

        private void EditMedicineClick(object sender, RoutedEventArgs e)
        {
            var id = (string)(sender as Button).Tag;
            this.MedicinesViewModel.EditableMedicine = new Medicine() { Id = id };
            this.EditPopup.IsOpen = true;
            Application.Current.MainWindow.IsEnabled = false;
        }

        private void CloseEditMedicinePopup_Click(object sender, RoutedEventArgs e)
        {
            this.EditPopup.IsOpen = false;
            Application.Current.MainWindow.IsEnabled = true;
        }
    }
}
