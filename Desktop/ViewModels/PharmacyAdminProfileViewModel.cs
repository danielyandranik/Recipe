using GalaSoft.MvvmLight;
using Desktop.Interfaces;
using Desktop.Services;
using Desktop.Validations;
using Desktop.Commands;
using UserManagementConsumer.Models;
using UserManagementConsumer.Client;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Pharmacy admin viewmodel
    /// </summary>
    public class PharmacyAdminProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// Pharmacy admin
        /// </summary>
        private PharmacyAdmin _pharmacyAdmin;

        /// <summary>
        /// Pharmacy admin input validation
        /// </summary>
        private readonly IValidation _pharmacyInputValidation;

        /// <summary>
        /// Pharmacy admin adding service
        /// </summary>
        private readonly IService<Response<string>> _pharmacyAdminService;

        /// <summary>
        /// Pharmacy admin command
        /// </summary>
        private readonly ProfileCommand<PharmacyAdmin> _pharmacyAdminCommand;

        /// <summary>
        /// Gets or sets pharmacy admin
        /// </summary>
        public PharmacyAdmin PharmacyAdmin
        {
            // gets pharmacy admin
            get => this._pharmacyAdmin;

            // sets pharmacy admin
            set => this.Set("PharmacyAdmin", ref this._pharmacyAdmin, value);
        }

        /// <summary>
        /// Gets pharmacy admin command
        /// </summary>
        public ProfileCommand<PharmacyAdmin> PharmacyAdminCommand => this._pharmacyAdminCommand;

        public PharmacyAdminProfileViewModel()
        {
            this._pharmacyAdmin = new PharmacyAdmin();
            this._pharmacyAdmin.UserId = User.Default.Id;
            this._pharmacyAdminService = new PharmacyAdminProfileService();
            this._pharmacyInputValidation = new PharmacyAdminInputValidation();
            this._pharmacyAdminCommand = new ProfileCommand<PharmacyAdmin>(
                this._pharmacyAdminService.Execute, 
                this._pharmacyInputValidation.Validate,
                "pharmacy_admin");
        }
    }
}
