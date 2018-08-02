using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Approve doctor command
    /// </summary>
    public class ApproveDoctorCommand : AsyncCommand<string, Response<IEnumerable<Profile>>>
    {
        /// <summary>
        /// Creates new instance of <see cref="ApproveDoctorCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public ApproveDoctorCommand(Func<string, Task<Response<IEnumerable<Profile>>>> executeMethod, Func<string, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {

        }
    }
}
