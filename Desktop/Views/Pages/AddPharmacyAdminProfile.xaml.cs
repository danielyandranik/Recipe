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
using Desktop.ViewModels;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddPharmacyAdmin.xaml
    /// </summary>
    public partial class AddPharmacyAdminProfile : Page
    {
        private readonly PharmacyAdminProfileViewModel _pharmacyAdminVM;

        public AddPharmacyAdminProfile()
        {
            InitializeComponent();
            this._pharmacyAdminVM = new PharmacyAdminProfileViewModel();
            this.DataContext = this._pharmacyAdminVM;
        }
    }
}
