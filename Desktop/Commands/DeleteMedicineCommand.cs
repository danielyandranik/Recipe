using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MedicineApiClient;

namespace Desktop.Commands
{
    /// <summary>
    /// Delete medicine command
    /// </summary>
    public class DeleteMedicineCommand : AsyncCommand<string, bool>
    {
        /// <summary>
        /// Medicines
        /// </summary>
        private ObservableCollection<Medicine> _medicines;
        
        /// <summary>
        /// Creates new instance of <see cref="DeleteMedicineCommand"/>
        /// </summary>
        /// <param name="medicines">Medicines</param>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method.</param>
        public DeleteMedicineCommand(ObservableCollection<Medicine> medicines, Func<string, Task<bool>> executeMethod, Func<string, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
            this._medicines = medicines;
        }

        /// <summary>
        /// Executes the command operation.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public async override void Execute(object parameter)
        {
            var isSuccessed = await this.ExecuteAsync($"api/medicines/{(string)parameter}");

            if (isSuccessed)
            {
                var response = await ((App)App.Current).MedicineClient.GetAllMedicinesAsync("api/medicines");
                if(response.IsSuccessStatusCode)
                {
                    this._medicines = new ObservableCollection<Medicine>(response.Result);
                }                
            }
        }
    }
}
