using Desktop.Views.Windows;
using System;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Command for adding hospital director profile
    /// </summary>
    public class HospitalDirectorProfileCommand : AsyncCommand<HospitalDirector, Response<string>>
    {

        /// <summary>
        /// Creates new instance of <see cref="HospitalDirectorProfileCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public HospitalDirectorProfileCommand(Func<HospitalDirector, Task<Response<string>>> executeMethod, Func<HospitalDirector, bool> canExecuteMethod) :
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
                var director = (HospitalDirector)parameter;

                var response = await this.ExecuteAsync(director);

                if (response.Status == Status.Ok)
                {
                    RecipeMessageBox.Show("Hospital director profile is added");
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
