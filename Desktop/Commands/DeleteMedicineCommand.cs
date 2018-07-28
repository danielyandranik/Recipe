using MedicineApiClient;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    public class DeleteMedicineCommand : AsyncCommand<string, bool>
    {
        private ObservableCollection<Medicine> _medicines;
        
        public DeleteMedicineCommand(ObservableCollection<Medicine> medicines, Func<string, Task<bool>> executeMethod, Func<string, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            this._medicines = medicines;
        }

        public async override void Execute(object parameter)
        {
            var isSuccessed = await this.ExecuteAsync($"api/medicines/{(string)parameter}");
            if(isSuccessed)
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
