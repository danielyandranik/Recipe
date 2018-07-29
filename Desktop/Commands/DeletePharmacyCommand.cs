using InstitutionClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    public class DeletePharmacyCommand : AsyncCommand<int, bool>
    {
        private ObservableCollection<Institution> pharmacies;

        public DeletePharmacyCommand(ObservableCollection<Institution> pharmacies, Func<int, Task<bool>> executeMethod, Func<int, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            this.pharmacies = pharmacies;
        }

        public async override void Execute(object parameter)
        {
            var isSuccessed = await this.ExecuteAsync((int)parameter);
            if (isSuccessed)
            {
                var response = await ((App)App.Current).InstitutionClient.GetAllPharmaciesAsync();
                if (response.IsSuccessStatusCode)
                {
                    this.pharmacies = new ObservableCollection<Institution>(response.Content);
                }
            }
        }
    }
}
