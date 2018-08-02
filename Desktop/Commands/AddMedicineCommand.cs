using System;
using System.Threading.Tasks;
using Desktop.Views.Windows;
using MedicineApiClient;

namespace Desktop.Commands
{
    /// <summary>
    /// Medicine Adding command
    /// </summary>
    class AddMedicineCommand : AsyncCommand<Medicine, bool>
    {
        /// <summary>
        /// Creates new instance of <see cref="AddMedicineCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method.</param>
        /// <param name="canExecuteMethod">Can execute method.</param>
        public AddMedicineCommand(Func<Medicine, Task<bool>> executeMethod, Func<Medicine, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
        }

        /// <summary>
        /// Executes command operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            var added = await this.ExecuteAsync((Medicine)parameter);

            if (added)
                RecipeMessageBox.Show("Medicine is added");
            else RecipeMessageBox.Show("Unable to add medicine");
        }
    }
}
