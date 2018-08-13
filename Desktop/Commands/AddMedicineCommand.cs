using System;
using System.Threading.Tasks;
using System.Windows;
using MedicineApiClient;
using Desktop.Views.Windows;
using Desktop.ViewModels;

namespace Desktop.Commands
{
    /// <summary>
    /// Medicine Adding command
    /// </summary>
    class AddMedicineCommand : AsyncCommand<Medicine, bool>
    {
        /// <summary>
        /// Add medicine page viewmodel
        /// </summary>
        private readonly AddMedicineViewModel _vm;

        /// <summary>
        /// Boolean value indicating whether Done button is available
        /// </summary>
        private bool _isDoneAvailable;

        /// <summary>
        /// Creates new instance of <see cref="AddMedicineCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method.</param>
        /// <param name="canExecuteMethod">Can execute method.</param>
        public AddMedicineCommand(AddMedicineViewModel addMedicineViewModel, Func<Medicine, Task<bool>> executeMethod, Func<Medicine, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
            this._vm = addMedicineViewModel;
            this._isDoneAvailable = true;
        }

        /// <summary>
        /// Executes command operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            if (!this._isDoneAvailable)
                return;

            this._isDoneAvailable = false;
            this._vm.SetVisibilities(Visibility.Visible, true);

            var dictionary = App.Current.Resources;

            try
            {
                var added = await this.ExecuteAsync((Medicine)parameter);

                if (added)
                    RecipeMessageBox.Show((string)dictionary["med_add_success"]);
                else RecipeMessageBox.Show((string)dictionary["med_add_fail"]);
            }
            catch
            {
                RecipeMessageBox.Show((string)dictionary["med_add_fail"]);
            }
            finally
            {
                this._isDoneAvailable = true;
                this._vm.SetVisibilities(Visibility.Collapsed, false);
            }
        }
    }
}
