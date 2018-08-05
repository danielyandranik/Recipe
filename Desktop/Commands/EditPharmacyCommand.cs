using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Desktop.Views.Windows;
using InstitutionClient.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Edit pharmacy command
    /// </summary>
    public class EditPharmacyCommand : AsyncCommand<Institution, bool>
    {
        /// <summary>
        /// Pharmacies
        /// </summary>
        private ObservableCollection<Institution> pharmacies;

        /// <summary>
        /// Creates new instance of <see cref="EditPharmacyCommand"/>
        /// </summary>
        /// <param name="pharmacies">Pharmacies</param>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public EditPharmacyCommand(ObservableCollection<Institution> pharmacies, Func<Institution, Task<bool>> executeMethod, Func<Institution, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
            this.pharmacies = pharmacies;
        }

        /// <summary>
        /// Executes the command operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            var dictionary = App.Current.Resources;

            try
            {
                var isSucceed = await this.ExecuteAsync(parameter as Institution);

                if (isSucceed)
                {
                    RecipeMessageBox.Show((string)dictionary["pharmacy_edit_success"]);

                    var response = await ((App)App.Current).InstitutionClient.GetAllPharmaciesAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        this.pharmacies = new ObservableCollection<Institution>(response.Content);
                    }
                }
                else
                {
                    RecipeMessageBox.Show((string)dictionary["pharmacy_edit_fail"]);
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show((string)dictionary["server_error"]);
            }
        }
    }
}
