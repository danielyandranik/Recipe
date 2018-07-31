using Desktop.ViewModels;
using MedicineApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            this.MedicinesViewModel = new MedicinesViewModel();
            this.DataContext = this.MedicinesViewModel;
            InitializeComponent();
        }

        private void EditMedicineClick(object sender, RoutedEventArgs e)
        {
            var id = (string)(sender as Button).Tag;
            this.MedicinesViewModel.EditableMedicine = new Medicine() { Id = id, Name="miban" };
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
