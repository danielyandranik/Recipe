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
    public class SignInViewModel:ViewModelBase
    {
        /// <summary>
        /// Sign In Text visibility
        /// </summary>
        private Visibility _signInTextVisibility;

        /// <summary>
        /// Spinner visibility
        /// </summary>
        private Visibility _spinnerVisibility;

        /// <summary>
        /// Is spinning value
        /// </summary>
        private bool _isSpinning;

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
        /// Gets or sets IsSpinning
        /// </summary>
        public bool IsSpinning
        {
            // gets IsSpinning
            get => this._isSpinning;

            // sets IsSpinning
            set => this.Set("IsSpinning", ref this._isSpinning, value);
        }

        /// <summary>
        /// Gets or sets spinner visibility
        /// </summary>
        public Visibility SpinnerVisibility
        {
            // gets spinner visibility
            get => this._spinnerVisibility;

            // sets spinner visibility
            set => this.Set("SpinnerVisibility", ref this._spinnerVisibility, value);
        }

        /// <summary>
        /// Gets or sets SignIn text visibility
        /// </summary>
        public Visibility SignInTextVisibility
        {
            // gets sign in text visibility
            get => this._signInTextVisibility;

            // sets sign in text visibility
            set => this.Set("SignInTextVisibility", ref this._signInTextVisibility, value);
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
            this.SetVisibilities(Visibility.Collapsed, Visibility.Visible, false);
        }

        /// <summary>
        /// Sets visibilities
        /// </summary>
        /// <param name="spinnerVisibility">Spinner visibility</param>
        /// <param name="signInTextVisibility">Sign In Text visibility</param>
        /// <param name="isSpinning">IsSpinning value</param>
        public void SetVisibilities(Visibility spinnerVisibility,Visibility signInTextVisibility,bool isSpinning)
        {
            this.SpinnerVisibility = spinnerVisibility;
            this.SignInTextVisibility = signInTextVisibility;
            this.IsSpinning = isSpinning;
        }
    }
}
