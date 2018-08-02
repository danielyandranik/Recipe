using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MedicineApiClient;

namespace Desktop.Commands
{
    /// <summary>
    /// Edit medicine command
    /// </summary>
    public class EditMedicineCommand : AsyncCommand<Medicine, bool>
    {
        /// <summary>
        /// Medicines
        /// </summary>
        private ObservableCollection<Medicine> _medicines;

        /// <summary>
        /// Creates new instance of <see cref="EditMedicineCommand"/>
        /// </summary>
        /// <param name="medicines">Medicines</param>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public EditMedicineCommand(ObservableCollection<Medicine> medicines, Func<Medicine, Task<bool>> executeMethod, Func<Medicine, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
            this._medicines = medicines;
        }

        /// <summary>
        /// Executes the command operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            var isSuccessed = await this.ExecuteAsync((Medicine)parameter);

            if (isSuccessed)
            {
                var response = await ((App)App.Current).MedicineClient.GetAllMedicinesAsync("api/medicines");
                if (response.IsSuccessStatusCode)
                {
                    this._medicines = new ObservableCollection<Medicine>(response.Result);
                }
            }
        }

    }
}
