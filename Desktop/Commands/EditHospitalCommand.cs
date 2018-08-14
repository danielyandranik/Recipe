using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InstitutionClient.Models;
using Desktop.Views.Windows;
using Desktop.ViewModels;

namespace Desktop.Commands
{
    /// <summary>
    /// Edit hospital command
    /// </summary>
    public class EditHospitalCommand : AsyncCommand<Institution, bool>
    {
        /// <summary>
        /// Hospitals page viewmodel
        /// </summary>
        private readonly HospitalsViewModel _vm;
        
        /// <summary>
        /// Creates new instance of <see cref="EditHospitalCommand"/>
        /// </summary>
        /// <param name="hospitalsViewModel">Hospitals page viewmodel</param>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public EditHospitalCommand(HospitalsViewModel hospitalsViewModel, Func<Institution, Task<bool>> executeMethod, Func<Institution, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
            this._vm = hospitalsViewModel;
        }

        /// <summary>
        /// Executes command operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            var dictionary = App.Current.Resources;

            try
            {
                var isSucceed = await this.ExecuteAsync(parameter as Institution);

                if (isSucceed)
                {
                    RecipeMessageBox.Show((string)dictionary["hospital_edit_success"]);
                    
                    var response = await ((App)App.Current).InstitutionClient.GetAllHospitalsAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        this._vm.Hospitals = new ObservableCollection<Institution>(response.Content);
                    }
                }
                else
                {
                    RecipeMessageBox.Show((string)dictionary["hospital_edit_fail"]);
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show((string)dictionary["server_error"]);
            }
        }
    }
}
