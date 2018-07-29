using MedicineApiClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    internal class EditMedicineCommand : AsyncCommand<Medicine, bool>
    {
        private ObservableCollection<Medicine> _medicines;

        public EditMedicineCommand(ObservableCollection<Medicine> medicines, Func<Medicine, Task<bool>> executeMethod, Func<Medicine, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            this._medicines = medicines;
        }

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
