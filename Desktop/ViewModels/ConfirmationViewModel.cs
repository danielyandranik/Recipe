using Desktop.Commands;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model for confirmation window
    /// </summary>
    public class ConfirmationViewModel:ViewModelBase
    {
        /// <summary>
        /// Verification code
        /// </summary>
        private string _verificationCode;

        /// <summary>
        /// Username
        /// </summary>
        private string _username;

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string Username
        {
            // gets username
            get => this._username;

            // sets username
            set => this._username = value;
        }

        /// <summary>
        /// Gets or sets verification code
        /// </summary>
        public string VerificationCode
        {
            // gets verification code
            get => this._verificationCode;

            // sets verification code
            set => this.SetProperty(ref this._verificationCode, value);
        }

        /// <summary>
        /// Gets or sets Confirm command
        /// </summary>
        public ICommand ConfirmCommand { get; private set; }

        public  ConfirmationViewModel(string username)
        {
            this._username = username;
            this._verificationCode = "";
            this.ConfirmCommand = new ConfirmCommand(this);
        }        
    }
}
