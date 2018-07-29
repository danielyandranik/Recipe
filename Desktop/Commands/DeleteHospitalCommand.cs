using InstitutionClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    public class DeleteHospitalCommand : AsyncCommand<int, bool>
    {
        private ObservableCollection<Institution> hospitals;

        public DeleteHospitalCommand(ObservableCollection<Institution> hospitals, Func<int, Task<bool>> executeMethod, Func<int, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            this.hospitals = hospitals;
        }

        public async override void Execute(object parameter)
        {
            var isSuccessed = await this.ExecuteAsync((int)parameter);
            if (isSuccessed)
            {
                var response = await ((App)App.Current).InstitutionClient.GetAllHospitalsAsync();
                if (response.IsSuccessStatusCode)
                {
                    this.hospitals = new ObservableCollection<Institution>(response.Content);
                }
            }
        }
    }
}
