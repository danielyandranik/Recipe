using Desktop.Interfaces;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;
using Desktop.Commands;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using Desktop.Views.Pages;
using Desktop.Validations;
using Desktop.Services;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model for adding hospital director profile page
    /// </summary>
    public class HospitalDirectorProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// Hospital director info
        /// </summary>
        private HospitalDirector director;

        /// <summary>
        /// Validation
        /// </summary>
        private readonly IValidation validation;

        /// <summary>
        /// Hospital director profile service
        /// </summary>
        private readonly IService<Response<string>> directorProfileService;

        // Gets or sets hospital director info
        public HospitalDirector Director
        {
            get => this.director;

            set => this.Set("Director", ref this.director, value);
        }

        /// <summary>
        /// Hospital director profile command
        /// </summary>
        private readonly ProfileCommand<HospitalDirector> directorProfileCommand;

        /// <summary>
        /// Gets Hospital director command
        /// </summary>
        public ICommand DirectorProfileCommand => this.directorProfileCommand;

        /// <summary>
        /// Gets or sets Add Hospital director profile page
        /// </summary>
        public AddHospitalAdministartorProfile DirectorProfile { get; private set; }

        /// <summary>
        /// Creates new instance of <see cref="AddHospitalAdministartorProfile"/>
        /// </summary>
        public HospitalDirectorProfileViewModel()
        {
            // setting fields

            this.director = new HospitalDirector();
            this.validation = new HospitalDirectorInputValidation();
            this.directorProfileService = new HospitalDirectorProfileService();
            this.directorProfileCommand = new ProfileCommand<HospitalDirector>(
                this.directorProfileService.Execute, 
                this.validation.Validate,
                "Hospital Admin");
        }
    }
}
