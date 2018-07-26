using System.Windows;
using Desktop.ViewModels;

namespace Desktop.Views.Windows
{
    /// <summary>
    /// Interaction logic for CodeConfirmation.xaml
    /// </summary>
    public partial class CodeConfirmation : Window
    {
        /// <summary>
        /// Code confirmation window view mode
        /// </summary>
        private readonly CodeConfirmationViewModel _codeConfirmationVM;

        /// <summary>
        /// Creates new instance of <see cref="CodeConfirmation"/>
        /// </summary>
        public CodeConfirmation(CodeConfirmationViewModel codeConfirmationViewModel)
        {
            // initializing components
            InitializeComponent();
            this.Owner = App.Current.Windows[0];
            this._codeConfirmationVM = codeConfirmationViewModel;
            this.DataContext = this._codeConfirmationVM;
        }
    }
}

