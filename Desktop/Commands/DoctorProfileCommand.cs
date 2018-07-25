using Desktop.Views.Windows;
using System;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Commands
{
    public class DoctorProfileCommand : AsyncCommand<Doctor, Response<string>>
    {

        /// <summary>
        /// Creates new instance of <see cref="DoctorProfileCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public DoctorProfileCommand(Func<Doctor, Task<Response<string>>> executeMethod, Func<Doctor, bool> canExecuteMethod) :
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
                var doctor = (Doctor)parameter;

                var response = await this.ExecuteAsync(doctor);

                if (response.Status == Status.Ok)
                {
                    RecipeMessageBox.Show("Doctor profile is added");
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
