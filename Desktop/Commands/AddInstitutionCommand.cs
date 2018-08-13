using System;
using System.Threading.Tasks;
using System.Windows;
using InstitutionClient.Models;
using Desktop.Views.Windows;
using Desktop.ViewModels;

namespace Desktop.Commands
{
    /// <summary>
    /// Institution add command
    /// </summary>
    public class AddInstitutionCommand : AsyncCommand<Institution, bool>
    {
        /// <summary>
        /// Boolean value indicating whether the done button is available
        /// </summary>
        private bool _isDoneAvailable;

        /// <summary>
        /// Add institution page viewmodel
        /// </summary>
        private readonly AddInstitutionViewModel _vm;

        /// <summary>
        /// Creates new instance of <see cref="AddInstitutionCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public AddInstitutionCommand(AddInstitutionViewModel addInstitutionViewModel, Func<Institution, Task<bool>> executeMethod, Func<Institution, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
            this._isDoneAvailable = true;
            this._vm = addInstitutionViewModel;
        }

        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            if (!this._isDoneAvailable)
                return;

            this._isDoneAvailable = false;
            this._vm.SetVisibilities(Visibility.Visible, true);

            var dictionary = App.Current.Resources;
            
            try
            {
                var institution = parameter as Institution;
                institution.Type = institution.Type == "0" ? "hospital" : "pharmacy";

                var isSucceed = await this.ExecuteAsync(institution);

                if (isSucceed)
                {
                    RecipeMessageBox.Show((string)dictionary["inst_add_success"]);
                }
                else
                {
                    RecipeMessageBox.Show((string)dictionary["inst_add_fail"]) ;
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show((string)dictionary["server_error"]);
            }
            finally
            {
                this._isDoneAvailable = true;
                this._vm.SetVisibilities(Visibility.Collapsed, false);
            }
        }
    }
}
