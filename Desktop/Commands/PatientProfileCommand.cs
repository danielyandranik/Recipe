using Desktop.Views.Windows;
using System;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Command for patient signing up
    /// </summary>
    public class PatientProfileCommand : AsyncCommand<Patient, Response<string>>
    {

        /// <summary>
        /// Creates new instance of <see cref="PatientProfileCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public PatientProfileCommand(Func<Patient, Task<Response<string>>> executeMethod, Func<Patient, bool> canExecuteMethod) :
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
                var patient = (Patient)parameter;

                var response = await this.ExecuteAsync(patient);

                if (response.Message.Equals("Success"))
                {
                    RecipeMessageBox.Show("Patient profile is added");
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
