using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using MedicineApiClient;
using Desktop.ViewModels;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    /// <summary>
    /// Delete medicine command
    /// </summary>
    public class DeleteMedicineCommand : AsyncCommand<string, bool>
    {
        /// <summary>
        /// Medicines Page viewmodel
        /// </summary>
        private readonly MedicinesViewModel _vm;
        
        /// <summary>
        /// Creates new instance of <see cref="DeleteMedicineCommand"/>
        /// </summary>
        /// <param name="medicines">Medicines</param>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method.</param>
        public DeleteMedicineCommand(MedicinesViewModel medicinesViewModel, Func<string, Task<bool>> executeMethod, Func<string, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
            this._vm = medicinesViewModel;
        }

        /// <summary>
        /// Executes the command operation.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public async override void Execute(object parameter)
        {
            var dictionary = App.Current.Resources;

            try
            {
                var isSuccessed = await this.ExecuteAsync($"api/medicines/{(string)parameter}");

                if (isSuccessed)
                {
                    RecipeMessageBox.Show((string)dictionary["med_del_success"]);

                    this._vm.SetVisibilities(Visibility.Visible, true);

                    var response = await ((App)App.Current).MedicineClient.GetAllMedicinesAsync("api/medicines");

                    if (response.IsSuccessStatusCode)
                    {
                        this._vm.Medicines = new ObservableCollection<Medicine>(response.Result);
                    }
                }
                else
                {
                    RecipeMessageBox.Show((string)dictionary["med_del_fail"]);
                }
            }
            catch
            {
                RecipeMessageBox.Show((string)dictionary["med_del_fail"]);
            }
            finally
            {
                this._vm.SetVisibilities(Visibility.Collapsed, false);
            }
        }
    }
}
