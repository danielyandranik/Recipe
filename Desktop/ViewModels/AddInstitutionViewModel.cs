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
    public class AddInstitutionViewModel : LoadablePageViewModel
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
        /// Add institution command
        /// </summary>
        private readonly AddInstitutionCommand addInstitutionCommand;

        /// <summary>
        /// Gets or sets institution info
        /// </summary>
        public Institution Institution
        {
            get => this.institution;

            set => this.Set("Institution", ref this.institution, value);
        }

        /// <summary>
        /// Gets add institution command
        /// </summary>
        public ICommand AddInstitutionCommand => this.addInstitutionCommand;
        
        /// <summary>
        /// Creates new instance of <see cref="AddInstitutionViewModel"/>
        /// </summary>
        public AddInstitutionViewModel()
        {
            // setting fields
            this.institution = new Institution();
            this.validation = new InstitutionInputValidation();
            this.institutionService = new AddInstitutionService();
            this.addInstitutionCommand = new AddInstitutionCommand(this,this.institutionService.Execute, this.validation.Validate);
        }
    }
}