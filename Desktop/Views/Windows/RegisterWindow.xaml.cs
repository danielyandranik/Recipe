using System.Windows;
using System.Windows.Controls;
using Desktop.ViewModels;

namespace Desktop.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        /// <summary>
        /// View model of Register window
        /// </summary>
        private RegisterWindowViewModel _registerViewModel;

        /// <summary>
        /// Creates new instance of <see cref="RegisterWindow"/>
        /// </summary>
        public RegisterWindow()
        {
            // initializing components
            InitializeComponent();
            this._registerViewModel = new RegisterWindowViewModel();

            // setting fields
            this.DataContext = this._registerViewModel;
        }

        /// <summary>
        /// Handler for password changed event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // converting sender to Password box
            var passwordBox = (PasswordBox)sender;

            // setting to view model fields
            if (passwordBox == this.password)
                this._registerViewModel.Register.Password = passwordBox.Password;
            else this._registerViewModel.Register.ConfirmPassword = passwordBox.Password;
        }
    }
}
