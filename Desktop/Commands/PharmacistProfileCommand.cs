using Desktop.Views.Windows;
using System;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Commands
{
    public class PatientProfileCommand : AsyncCommand<PharmacistFullInfo, Response<string>>
    {

        /// <summary>
        /// Creates new instance of <see cref="PatientProfileCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public PatientProfileCommand(Func<PharmacistFullInfo, Task<Response<string>>> executeMethod, Func<PharmacistFullInfo, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        { }

        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public override async void Execute(object parameter)
        {
            try
            {
                var pharmacist = (PharmacistFullInfo)parameter;

                var response = await this.ExecuteAsync(pharmacist);

                if (response.Message.Equals("Success"))
                {
                    RecipeMessageBox.Show("Pharmasist profile is added");
                }
                else
                {
                    RecipeMessageBox.Show("Unable to add profile");
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }
    }
}
