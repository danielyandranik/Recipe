using System.Windows;
using Desktop.ViewModels;

namespace Desktop.Views
{
    /// <summary>
    /// Interaction logic for CodeConfirmation.xaml
    /// </summary>
    public partial class CodeConfirmation : Window
    {
        /// <summary>
        /// Confirmation view model
        /// </summary>
        private ConfirmationViewModel _confirmationViewModel;

        /// <summary>
        /// Creates new instance of <see cref="CodeConfirmation"/>
        /// </summary>
        public CodeConfirmation(ConfirmationViewModel confirmationViewModel)
        {
            // initializing components
            InitializeComponent();

            // setting fields
            this._confirmationViewModel = confirmationViewModel;
            this.DataContext = this._confirmationViewModel;
        }
    }
}

