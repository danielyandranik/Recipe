using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Client;

namespace Desktop.Commands
{
    public class ApprovePharmacyAdminCommand : AsyncCommand<int, Response<string>>
    {
        public ApprovePharmacyAdminCommand(Func<int, Task<Response<string>>> executeMethod, Func<int, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
        }
    }
}
