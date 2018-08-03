using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Views.Windows;
using InstitutionClient.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Delete pharmacy command
    /// </summary>
    public class DeletePharmacyCommand : AsyncCommand<int, bool>
    {
        /// <summary>
        /// Pharmacies
        /// </summary>
        private ObservableCollection<Institution> pharmacies;

        /// <summary>
        /// Creates new instance of <see cref="DeletePharmacyCommand"/>
        /// </summary>
        /// <param name="pharmacies">Pharmacies</param>
        /// <param name="executeMethod">Execute method.</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public DeletePharmacyCommand(ObservableCollection<Institution> pharmacies, Func<int, Task<bool>> executeMethod, Func<int, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
            this.pharmacies = pharmacies;
        }

        /// <summary>
        /// Executes the command operation.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            try
            {
                var isSuccessed = await this.ExecuteAsync((int)parameter);
                
                if (isSuccessed)
                {
                    RecipeMessageBox.Show("Pharmacy removed successfully");

                    var response = await ((App)App.Current).InstitutionClient.GetAllPharmaciesAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        this.pharmacies = new ObservableCollection<Institution>(response.Content);
                    }
                }
                else
                {
                    RecipeMessageBox.Show("Unable to remove pharmacy");
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }
    }
}
