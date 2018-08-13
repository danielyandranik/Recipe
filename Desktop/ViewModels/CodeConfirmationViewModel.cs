using System.Net.Http;
using System.Windows.Input;
using System.Windows;
using Desktop.Commands;
using Desktop.Interfaces;
using Desktop.Services;
using Desktop.Validations;
using GalaSoft.MvvmLight;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Code confirmation view mode
    /// </summary>
    public class CodeConfirmationViewModel : LoadableWindowViewModel
    {
        /// <summary>
        /// User verification info
        /// </summary>
        private  UserVerificationInfo _userVerificationInfo;

        /// <summary>
        /// Confirm command
        /// </summary>
        private readonly ConfirmCommand _confirmCommand;

        /// <summary>
        /// Verification service
        /// </summary>
        private readonly IService<Response<HttpResponseMessage>> _verificationService;

        /// <summary>
        /// Validation for verification code
        /// </summary>
        private readonly IValidation _codeValidation;

        /// <summary>
        /// Gets or sets verification code
        /// </summary>
        public UserVerificationInfo VerificationInfo
        {
            // gets verification code
            get => this._userVerificationInfo;

            // sets verification code
            set => this.Set("VerificationCode", ref this._userVerificationInfo, value);
        }

        /// <summary>
        /// Gets confirm command
        /// </summary>
        public ICommand ConfirmCommand => this._confirmCommand;

        /// <summary>
        /// Creates new instance of <see cref="CodeConfirmationViewModel"/>
        /// </summary>
        /// <param name="username">username</param>
        public CodeConfirmationViewModel(string username)
        {
            this._verificationService = new CodeConfirmationService();
            this._codeValidation = new ConfirmationCodeInputValidation();
            this._userVerificationInfo = new UserVerificationInfo();
            this._userVerificationInfo.Username = username;
            this._userVerificationInfo.VerifyKey = "";
            this._confirmCommand = new ConfirmCommand(this,this._verificationService.Execute, this._codeValidation.Validate);
        }
    }
}
