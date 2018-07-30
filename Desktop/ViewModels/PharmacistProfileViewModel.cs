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
        private PharmacistFullInfo pharmacist;

        /// <summary>
        /// Validation
        /// </summary>
        private readonly IValidation validation;

        /// <summary>
        /// Pharmasist profile service
        /// </summary>
        private readonly IService<Response<string>> pharmasistProfileService;

        /// <summary>
        ///  Gets or sets patient info
        /// </summary>
        public PharmacistFullInfo Pharmacist
        {
            get => this.pharmacist;

            set => this.Set("Pharmacist", ref this.pharmacist, value);
        }

        /// <summary>
        /// Pharmasist profile command
        /// </summary>
        private readonly ProfileCommand<PharmacistFullInfo> pharmacistProfileCommand;

        /// <summary>
        /// Gets pharmasist profile command
        /// </summary>
        public ICommand PharmacistProfileCommand => this.pharmacistProfileCommand;

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
            this.pharmacist = new PharmacistFullInfo();
            this.pharmacist.UserId = User.Default.Id;
            this.validation = new PatientInputValidation();
            this.pharmasistProfileService = new PharmacistProfileService();
            this.pharmacistProfileCommand = new ProfileCommand<PharmacistFullInfo>(
                this.pharmasistProfileService.Execute, 
                this.validation.Validate,
                "Pharmacist");
        }
    }
}
