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
    /// <summary>
    /// View model for adding doctor profile page
    /// </summary>
    public class PharmacistProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// Doctor info
        /// </summary>
        private PharmacistFullInfo pharmacist;

        /// <summary>
        /// Validation
        /// </summary>
        private readonly IValidation validation;

        /// <summary>
        /// Doctor profile service
        /// </summary>
        private readonly IService<Response<string>> pharmacistProfileService;

        /// <summary>
        ///  Gets or sets doctor info
        /// </summary>
        public PharmacistFullInfo Pharmacist
        {
            get => this.pharmacist;

            set => this.Set("Pharmacist", ref this.pharmacist, value);
        }

        /// <summary>
        /// Doctor profile command
        /// </summary>
        private readonly ProfileCommand<PharmacistFullInfo> pharmacistProfileCommand;

        /// <summary>
        /// Gets pharmasist profile command
        /// </summary>
        public ICommand PharmacistProfileCommand => this.pharmacistProfileCommand;
        
        /// <summary>
        /// Creates new instance of <see cref="PharmacistProfileViewModel"/>
        /// </summary>
        public PharmacistProfileViewModel()
        {
            // setting fields
            this.pharmacist = new PharmacistFullInfo();
            this.pharmacist.UserId = User.Default.Id;
            this.validation = new PharmacistInputValidation();
            this.pharmacistProfileService = new PharmacistProfileService();
            this.pharmacistProfileCommand = new ProfileCommand<PharmacistFullInfo>(
                this.pharmacistProfileService.Execute,
                this.validation.Validate,
                "Pharmacist");
        }
    }
}
