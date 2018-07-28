using System;
using System.Windows;
using System.Windows.Controls;
using Desktop.ViewModels;

namespace Desktop.Views.Windows
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        /// <summary>
        /// Sign in view model
        /// </summary>
        private SignInViewModel _signInVM;

        public SignIn()
        {
            InitializeComponent();
            this._signInVM = new SignInViewModel();
            this.DataContext = this._signInVM;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;

            this._signInVM.SignInInfo.Password = passwordBox.Password;
        }
    }
}
