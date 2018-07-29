using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddHospitalAdministartorProfile.xaml
    /// </summary>
    public partial class AddHospitalAdministartorProfile : Page
    {
        private readonly HospitalDirectorProfileViewModel directorProfileViewModel;

        public AddHospitalAdministartorProfile()
        {
            InitializeComponent();

            this.directorProfileViewModel = new HospitalDirectorProfileViewModel();
            this.DataContext = this.directorProfileViewModel;
        }
    }
}
