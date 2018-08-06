using System;
using System.Threading.Tasks;
using Desktop.Views.Windows;
using InstitutionClient.Models;

namespace Desktop.Commands
{
    public class AddInstitutionCommand : AsyncCommand<Institution, bool>
    {
        /// <summary>
        /// Creates new instance of <see cref="AddInstitutionCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public AddInstitutionCommand(Func<Institution, Task<bool>> executeMethod, Func<Institution, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        { }

        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            var dictionary = ((App)App.Current).Resources;
            
            try
            {
                var institution = parameter as Institution;
                institution.Type = (string)dictionary[institution.Type];

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
        }
    }
}
