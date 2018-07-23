using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Desktop.ViewModels;

namespace Desktop.Views
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
