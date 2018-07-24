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
    public class DoctorProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// Doctor info
        /// </summary>
        private Doctor _doctor;

        /// <summary>
        /// Validation
        /// </summary>
        private IValidation _validation;

        /// <summary>
        /// Doctor profile service
        /// </summary>
        private readonly IService<Response<string>> _doctorProfileService;

        /// <summary>
        ///  Gets or sets doctor info
        /// </summary>
        public Doctor Doctor
        {
            get => this._doctor;

            set => this.Set("Doctor", ref this._doctor, value);
        }

        /// <summary>
        /// Doctor profile command
        /// </summary>
        private readonly DoctorProfileCommand _doctorProfileCommand;

        /// <summary>
        /// Gets pharmasist profile command
        /// </summary>
        public ICommand PharmacistProfileCommand => this._doctorProfileCommand;

        /// <summary>
        /// Gets or sets Add patient profile page
        /// </summary>
        public AddDoctorProfile DoctorProfile { get; private set; }

        /// <summary>
        /// Creates new instance of <see cref="AddDoctorProfile"/>
        /// </summary>
        public DoctorProfileViewModel()
        {
            // setting fields

            // TODO
            this.Doctor.UserId = 1;

            this._doctor = new Doctor();
            this._validation = new DoctorInputValidation();
            this._doctorProfileService = new DoctorProfileService();
            this._doctorProfileCommand = new DoctorProfileCommand(this._doctorProfileService.Execute, this._validation.Validate);
        }
    }
}
