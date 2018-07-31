using Desktop.ViewModels;
using System.Windows.Controls;
namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddInstitution.xaml
    /// </summary>
    public partial class AddInstitution : Page
    {
        private readonly AddInstitutionViewModel addInstitutionViewModel;

        public AddInstitution()
        {
            InitializeComponent();

            this.addInstitutionViewModel = new AddInstitutionViewModel();
            this.DataContext = this.addInstitutionViewModel;
        }
    }
}
