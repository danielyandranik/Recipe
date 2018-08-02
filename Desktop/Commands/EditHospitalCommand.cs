using Desktop.Views.Windows;
using InstitutionClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    public class EditHospitalCommand : AsyncCommand<Institution, bool>
    {
        private ObservableCollection<Institution> hospitals;

        public EditHospitalCommand(ObservableCollection<Institution> hospitals, Func<Institution, Task<bool>> executeMethod, Func<Institution, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            this.hospitals = hospitals;
        }

        public async override void Execute(object parameter)
        {
            try
            {
                var isSucceed = await this.ExecuteAsync(parameter as Institution);
                if (isSucceed)
                {
                    RecipeMessageBox.Show("Hospital updated successfully");
                    
                    var response = await ((App)App.Current).InstitutionClient.GetAllHospitalsAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        this.hospitals = new ObservableCollection<Institution>(response.Content);
                    }
                }
                else
                {
                    RecipeMessageBox.Show("Unable to update hospital");
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }
    }
}
