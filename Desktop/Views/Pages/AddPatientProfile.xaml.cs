using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddPatientProfile.xaml
    /// </summary>
    public partial class AddPatientProfile : Page
    {
        private PatientProfileViewModel _patientVM;

        public AddPatientProfile()
        {
            InitializeComponent();

            this._patientVM = new PatientProfileViewModel();
            this.DataContext = this._patientVM;
        }
    }
}
