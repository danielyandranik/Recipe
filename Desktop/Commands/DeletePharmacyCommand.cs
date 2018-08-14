using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InstitutionClient.Models;
using Desktop.ViewModels;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    /// <summary>
    /// Delete pharmacy command
    /// </summary>
    public class DeletePharmacyCommand : AsyncCommand<int, bool>
    {
        /// <summary>
        /// Pharmacies page viewmodel
        /// </summary>
        private readonly PharmaciesViewModel _vm;

        /// <summary>
        /// Creates new instance of <see cref="DeletePharmacyCommand"/>
        /// </summary>
        /// <param name="pharmacies">Pharmacies</param>
        /// <param name="executeMethod">Execute method.</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public DeletePharmacyCommand(PharmaciesViewModel pharmaciesViewModel, Func<int, Task<bool>> executeMethod, Func<int, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
            this._vm = pharmaciesViewModel;
        }

        /// <summary>
        /// Executes the command operation.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            var dictionary = App.Current.Resources;

            try
            {
                var isSuccessed = await this.ExecuteAsync((int)parameter);
                
                if (isSuccessed)
                {
                    RecipeMessageBox.Show((string)dictionary["pharmacy_del_success"]);

                    var response = await ((App)App.Current).InstitutionClient.GetAllPharmaciesAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        this._vm.Pharmacies = new ObservableCollection<Institution>(response.Content);
                    }
                }
                else
                {
                    RecipeMessageBox.Show((string)dictionary["pharmacy_del_fail"]);
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show((string)dictionary["server_error"]);
            }
        }
    }
}
