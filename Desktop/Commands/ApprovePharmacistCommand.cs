using Desktop.Services;
using Desktop.ViewModels;
using Desktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Client;

namespace Desktop.Commands
{
    public class ApprovePharmacistCommand : AsyncCommand<int, Response<string>>
    {
        private readonly PharmacyAdminApprovalsViewModel viewModel;

        public ApprovePharmacistCommand(PharmacyAdminApprovalsViewModel viewModel, Func<int, Task<Response<string>>> executeMethod, Func<int, bool> canExecuteMethod) :
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

            var service = new LoadPharmacyAdminApprovalsService(this.viewModel);
            await service.Load();
        }
    }
}
