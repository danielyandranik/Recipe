using Desktop.Interfaces;
using System;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    public class PharmacistProfileService : IService<Response<string>>
    {
        /// <summary>
        /// User management API client
        /// </summary>
        private readonly UserManagementApiClient _userManagementApiClient;

        /// <summary>
        /// Creates new instance of <see cref="PharmacistProfileService"/>
        /// </summary>
        public PharmacistProfileService()
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
            return await this._userManagementApiClient.PostPharmacistAsync((PharmacistFullInfo)parameter);
        }
    }
}
