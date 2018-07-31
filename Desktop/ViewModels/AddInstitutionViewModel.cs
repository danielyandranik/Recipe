using Desktop.Commands;
using Desktop.Interfaces;
using Desktop.Services;
using Desktop.Validations;
using Desktop.Views.Pages;
using GalaSoft.MvvmLight;
using InstitutionClient.Models;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public class AddInstitutionViewModel : ViewModelBase
    {
        /// <summary>
        /// Institution info
        /// </summary>
        private Institution institution;
        
        /// <summary>
        /// Validation
        /// </summary>
        private readonly IValidation validation;

        /// <summary>
        /// Institution service
        /// </summary>
        private readonly IService<bool> institutionService;

        /// <summary>
        /// Gets or sets institution info
        /// </summary>
        public Institution Institution
        {
            get => this.institution;

            set => this.Set("Institution", ref this.institution, value);
        }

        /// <summary>
        /// Add institution command
        /// </summary>
        private readonly AddInstitutionCommand addInstitutionCommand;

        /// <summary>
        /// Gets add institution command
        /// </summary>
        public ICommand AddInstitutionCommand => this.addInstitutionCommand;

        /// <summary>
        /// Gets or sets Add institution page
        /// </summary>
        public AddInstitution AddInstitution { get; private set; }

        /// <summary>
        /// Creates new instance of <see cref="AddInstitutionViewModel"/>
        /// </summary>
        public AddInstitutionViewModel()
        {
            // setting fields
            this.institution = new Institution();

            // TODO
            this.institution.Id = User.Default.Id;

            this.validation = new InstitutionInputValidation();
            this.institutionService = new AddInstitutionService();
            this.addInstitutionCommand = new AddInstitutionCommand(this.institutionService.Execute, this.validation.Validate);
        }
    }
}