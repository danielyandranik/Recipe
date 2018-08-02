using Desktop.Views.Windows;
using InstitutionClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    public class EditPharmacyCommand : AsyncCommand<Institution, bool>
    {
        private ObservableCollection<Institution> pharmacies;

        public EditPharmacyCommand(ObservableCollection<Institution> pharmacies, Func<Institution, Task<bool>> executeMethod, Func<Institution, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            this.pharmacies = pharmacies;
        }

        public async override void Execute(object parameter)
        {
            try
            {
                var isSucceed = await this.ExecuteAsync(parameter as Institution);
                if (isSucceed)
                {
                    RecipeMessageBox.Show("Pharmacy updated successfully");

                    var response = await ((App)App.Current).InstitutionClient.GetAllPharmaciesAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        this.pharmacies = new ObservableCollection<Institution>(response.Content);
                    }
                }
                else
                {
                    RecipeMessageBox.Show("Unable to update pharmacy");
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }
    }
}
