using System.Net.Http;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Desktop.Interfaces;
using Desktop.Validations;
using Desktop.Services;
using Desktop.Views.Windows;
using Desktop.Models;
using Desktop.Commands;
using UserManagementConsumer.Client;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model for register window
    /// </summary>
    public class RegisterWindowViewModel : LoadableWindowViewModel
    {
        /// <summary>
        /// Register info
        /// </summary>
        private Register _registerInfo;

        /// <summary>
        /// Validation
        /// </summary>
        private readonly IValidation _validation;

        /// <summary>
        /// Registration service
        /// </summary>
        private readonly IService<Response<HttpResponseMessage>> _registrationService;

        /// <summary>
        /// Register command
        /// </summary>
        private readonly RegisterCommand _registerCommand;

        /// <summary>
        /// Hyper link command
        /// </summary>
        private readonly RelayCommand _hyperlinkCommand;

        /// <summary>
        /// Hyper link service
        /// </summary>
        private readonly HyperLinkService _hyperLinkService;

        /// <summary>
        /// Gets or sets register info
        /// </summary>
        public Register Register
        {
            // gets register info
            get => this._registerInfo;

            // sets register info
            set => this.Set("Register", ref this._registerInfo, value);
        }

        /// <summary>
        /// Gets register commad
        /// </summary>
        public ICommand RegisterCommand => this._registerCommand;

        /// <summary>
        /// Gets hyper link command
        /// </summary>
        public ICommand HyperLinkCommand => this._hyperlinkCommand;

        /// <summary>
        /// Creates new instance of <see cref="RegisterWindowViewModel"/>
        /// </summary>
        public RegisterWindowViewModel()
        {
            // setting fields
            this._registerInfo = new Register();
            this._registerInfo.SexIndex = -1;
            this._validation = new RegisterInfputValidation();
            this._registrationService = new RegistrationService();
            this._hyperLinkService = new HyperLinkService();
            this._registerCommand = new RegisterCommand(this,this._registrationService.Execute, this._validation.Validate);
            this._hyperlinkCommand = new RelayCommand(() => this._hyperLinkService.Navigate<RegisterWindow,SignIn>(), () => true);
        }
    }
}
