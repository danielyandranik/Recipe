using System;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using Desktop.Views.Windows;
using Desktop.Services;
using Desktop.ViewModels;

namespace Desktop.Commands
{
    /// <summary>
    /// Approve doctor command
    /// </summary>
    public class ApproveDoctorCommand : AsyncCommand<int, Response<string>>
    {
        private readonly HospitalAdminApprovalViewModel viewModel;

        /// <summary>
        /// Creates new instance of <see cref="ApproveDoctorCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public ApproveDoctorCommand(HospitalAdminApprovalViewModel viewModel, Func<int, Task<Response<string>>> executeMethod, Func<int, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
            this.viewModel = viewModel;
        }

        /// <summary>
        /// Executes doctor approving command
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public override async void Execute(object parameter)
        {
            var response = await this.ExecuteAsync((int)parameter);
            
            if(response.Status == Status.Error)
            {
                RecipeMessageBox.Show("Error occured");
                return;
            }

            RecipeMessageBox.Show("Profile is successfully approved");

            var service = new LoadHospitalAdminApprovalsService(this.viewModel);
            await service.Load();
        }
    }
}
