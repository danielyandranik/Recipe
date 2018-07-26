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
        private Doctor doctor;

        /// <summary>
        /// Validation
        /// </summary>
        private IValidation validation;

        /// <summary>
        /// Doctor profile service
        /// </summary>
        private readonly IService<Response<string>> doctorProfileService;

        /// <summary>
        ///  Gets or sets doctor info
        /// </summary>
        public Doctor Doctor
        {
            get => this.doctor;

            set => this.Set("Doctor", ref this.doctor, value);
        }

        /// <summary>
        /// Doctor profile command
        /// </summary>
        private readonly ProfileCommand<Doctor> doctorProfileCommand;

        /// <summary>
        /// Gets pharmasist profile command
        /// </summary>
        public ICommand DoctorProfileCommand => this.doctorProfileCommand;

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
            this.doctor = new Doctor();
            this.doctor.UserId = User.Default.Id;
            this.validation = new DoctorInputValidation();
            this.doctorProfileService = new DoctorProfileService();
            this.doctorProfileCommand = new ProfileCommand<Doctor>(this.doctorProfileService.Execute, this.validation.Validate);
        }
    }
}
