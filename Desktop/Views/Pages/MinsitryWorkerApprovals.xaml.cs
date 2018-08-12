using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for MinsitryWorkerApprovements.xaml
    /// </summary>
    public partial class MinistryWorkerApprovals : Page
    {
        private readonly MinistryWorkerApprovalsViewModel viewModel;

        public MinistryWorkerApprovals()
        {
            InitializeComponent();
            this.viewModel = new MinistryWorkerApprovalsViewModel();
            this.DataContext = this.viewModel;
        }
    }
}
