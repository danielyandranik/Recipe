using Desktop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    class PatientProfileService : IService<Response<string>>
    {
        /// <summary>
        /// User management API client
        /// </summary>
        private readonly UserManagementApiClient _userManagementApiClient;

        /// <summary>
        /// Creates new instance of <see cref="RegistrationService"/>
        /// </summary>
        public PatientProfileService()
        {
            this._userManagementApiClient = new UserManagementApiClient();
            this._userManagementApiClient.SignInAsync("sona", "sona");
        }

        /// <summary>
        /// Executes the service
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>result</returns>
        public async Task<Response<string>> Execute(object parameter)
        {
            return await this._userManagementApiClient.PostPatientAsync((Patient)parameter);
        }
    }
}
