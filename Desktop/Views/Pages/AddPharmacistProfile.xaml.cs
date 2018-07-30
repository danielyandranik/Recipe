using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddPharmacistProfile.xaml
    /// </summary>
    public partial class AddPharmacistProfile : Page
    {
        private readonly PharmacistProfileViewModel pharmacistVM;

        public AddPharmacistProfile()
        {
            InitializeComponent();

            this.pharmacistVM = new PharmacistProfileViewModel();
            this.DataContext = this.pharmacistVM;
        }
    }
}
