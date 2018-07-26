using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for Add_DoctorProfile.xaml
    /// </summary>
    public partial class AddDoctorProfile : Page
    {
        private readonly DoctorProfileViewModel doctorProfileViewModel;

        public AddDoctorProfile()
        {
            InitializeComponent();

            this.doctorProfileViewModel = new DoctorProfileViewModel();
            this.DataContext = this.doctorProfileViewModel;
        }
    }
}
