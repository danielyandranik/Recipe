using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Commands
{
    public class ApproveDoctorCommand : AsyncCommand<string, Response<IEnumerable<Profile>>>
    {
        public ApproveDoctorCommand(Func<string, Task<Response<IEnumerable<Profile>>>> executeMethod, Func<string, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {

        }
    }
}
