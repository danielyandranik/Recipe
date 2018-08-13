using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Desktop.Views.Windows;
using Desktop.Services;
using Desktop.Models;
using Desktop.Commands;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Sign in View mode
    /// </summary>
    public class SignInViewModel : LoadableWindowViewModel
    {
        /// <summary>
        /// Sign in information
        /// </summary>
        private SignInInfo _signInInfo;

        /// <summary>
        /// Hyper link command
        /// </summary>
        private readonly RelayCommand _hyperLinkCommand;

        /// <summary>
        /// Hyper link service
        /// </summary>
        private readonly HyperLinkService _hyperLinkService;

        /// <summary>
        /// SignIn command
        /// </summary>
        private readonly SignInCommand _signInCommand;

        /// <summary>
        /// Gets or sets sign in info
        /// </summary>
        public SignInInfo SignInInfo
        {
            // gets sign in info
            get => this._signInInfo;

            // sets sign in info
            set => this.Set("SignInInfo", ref this._signInInfo, value);
        }

        /// <summary>
        /// Gets hyper link command
        /// </summary>
        public ICommand HyperLinkCommand => this._hyperLinkCommand;

        /// <summary>
        /// Gets sign in command
        /// </summary>
        public ICommand SignInCommand => this._signInCommand;

        /// <summary>
        /// Creates new instance of <see cref="SignInViewModel"/>
        /// </summary>
        public SignInViewModel()
        {
            this._signInInfo = new SignInInfo();
            this._hyperLinkService = new HyperLinkService();
            this._hyperLinkCommand = new RelayCommand(() => this._hyperLinkService.Navigate<SignIn,RegisterWindow>(),() => true);
            this._signInCommand = new SignInCommand(this);
        }
    }
}
