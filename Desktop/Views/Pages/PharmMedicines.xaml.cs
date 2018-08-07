using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for PharmMedicines.xaml
    /// </summary>
    public partial class PharmMedicines : Page
    {
        public PharmMedicinesViewModel MedicinesViewModel { get; private set; }

        public PharmMedicines()
        {
            this.MedicinesViewModel = new PharmMedicinesViewModel();
            this.DataContext = this.MedicinesViewModel;
            InitializeComponent();
        }
    }
}
