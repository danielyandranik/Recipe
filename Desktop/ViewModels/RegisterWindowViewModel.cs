using System.Windows.Input;
using Desktop.Views;
using Desktop.Models;
using Desktop.Commands;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model for register window
    /// </summary>
    public class RegisterWindowViewModel: ViewModelBase
    {
        /// <summary>
        /// Registration information
        /// </summary>
        private PatientInfo _register;        

        /// <summary>
        /// Gets or sets Register command
        /// </summary>
        public ICommand RegisterCommand { get; private set; }

        /// <summary>
        /// Gets or sets Hyper link command
        /// </summary>
        public ICommand HyperLinkCommand { get; private set; }

        /// <summary>
        /// Gets or sets Register window
        /// </summary>
        public RegisterWindow RegisterWindow { get; private set; }

        /// <summary>
        /// Gets or sets Register information
        /// </summary>
        public PatientInfo Register
        {
            // gets register information
            get => this._register;

            // sets register information
            set => this.SetProperty(ref this._register, value);
        }

        /// <summary>
        /// Creates new instance of <see cref="RegisterWindowViewModel"/>
        /// </summary>
        /// <param name="registerWindow">Register windoe</param>
        public RegisterWindowViewModel(RegisterWindow registerWindow)
        {
            // setting fields and properties
            this._register = new PatientInfo();
            this.RegisterWindow = registerWindow;
            this.RegisterCommand = new RegisterCommand();
            this.HyperLinkCommand = new HyperLinkCommand();
        }
    }
}
