using Desktop.Services;
using Desktop.ViewModels;
using Desktop.Views.Windows;
using System;
using System.Threading.Tasks;
using UserManagementConsumer.Client;

namespace Desktop.Commands
{
    public class ApprovePharmacyAdminCommand : AsyncCommand<int, Response<string>>
    {
        private readonly MinistryWorkerApprovalsViewModel viewModel;

        public ApprovePharmacyAdminCommand( MinistryWorkerApprovalsViewModel viewModel, 
                                            Func<int, Task<Response<string>>> executeMethod, 
                                            Func<int, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
            this.viewModel = viewModel;
        }

        public override async void Execute(object parameter)
        {
            var response = await this.ExecuteAsync((int)parameter);

            if (response.Status == Status.Error)
            {
                RecipeMessageBox.Show("Error occured");
                return;
            }

            RecipeMessageBox.Show("Profile is successfully approved");

            var service = new LoadUnapprovedPharmacyAdminsService(this.viewModel);
            await service.Load();
        }
    }
}
