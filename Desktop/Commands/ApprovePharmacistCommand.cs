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
        public ApprovePharmacistCommand(Func<int, Task<Response<string>>> executeMethod, Func<int, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
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
        }
    }
}
