using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for PharmacyAdminApprovals.xaml
    /// </summary>
    public partial class PharmacyAdminApprovals : Page
    {       
        public PharmacyAdminApprovals()
        {
            InitializeComponent();
            this.DataContext = new PharmacyAdminApprovalsViewModel();
        }
    }
}
