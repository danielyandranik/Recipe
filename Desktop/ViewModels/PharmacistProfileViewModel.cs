using Desktop.Commands;
using Desktop.Interfaces;
using Desktop.Services;
using Desktop.Validations;
using Desktop.Views.Pages;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.ViewModels
{
    public class PharmacistProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// Pharmasist info
        /// </summary>
        private PharmacistFullInfo _pharmacist;

        /// <summary>
        /// Validation
        /// </summary>
        private readonly IValidation _validation;

        /// <summary>
        /// Pharmasist profile service
        /// </summary>
        private readonly IService<Response<string>> _pharmasistProfileService;

        /// <summary>
        ///  Gets or sets patient info
        /// </summary>
        public PharmacistFullInfo Pharmacist
        {
            get => this._pharmacist;

            set => this.Set("PharmacistFullInfo", ref this._pharmacist, value);
        }

        /// <summary>
        /// Pharmasist profile command
        /// </summary>
        private readonly PharmacistProfileCommand _pharmacistProfileCommand;

        /// <summary>
        /// Gets pharmasist profile command
        /// </summary>
        public ICommand PharmacistProfileCommand => this._pharmacistProfileCommand;

        /// <summary>
        /// Gets or sets Add patient profile page
        /// </summary>
        public AddPharmacistProfile PharmacistProfile { get; private set; }

        /// <summary>
        /// Creates new instance of <see cref="AddPharmacistProfile"/>
        /// </summary>
        public PharmacistProfileViewModel()
        {
            // setting fields
            this._pharmacist = new PharmacistFullInfo();
            this._pharmacist.UserId = User.Default.Id;
            this._validation = new PatientInputValidation();
            this._pharmasistProfileService = new PharmacistProfileService();
            this._pharmacistProfileCommand = new PharmacistProfileCommand(this._pharmasistProfileService.Execute, this._validation.Validate);
        }
    }
}
