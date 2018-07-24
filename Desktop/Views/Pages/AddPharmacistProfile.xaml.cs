using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddPharmacistProfile.xaml
    /// </summary>
    public partial class AddPharmacistProfile : Page
    {
        private PharmacistProfileViewModel _pharmacistVM;

        public AddPharmacistProfile()
        {
            InitializeComponent();

            this._pharmacistVM = new PharmacistProfileViewModel();
            this.DataContext = this._pharmacistVM;
        }
    }
}
