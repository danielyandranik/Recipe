using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Client;

namespace Desktop.Commands
{
    public class ApproveHospitalAdminCommand : AsyncCommand<int, Response<string>>
    {
        public ApproveHospitalAdminCommand(Func<int, Task<Response<string>>> executeMethod, Func<int, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }
    }
}
