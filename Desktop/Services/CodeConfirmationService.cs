using System.Net.Http;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;
using Desktop.Interfaces;

namespace Desktop.Services
{
    /// <summary>
    /// Code confirmation service
    /// </summary>
    public class CodeConfirmationService : IService<Response<HttpResponseMessage>>
    {
        /// <summary>
        /// User management API client
        /// </summary>
        private readonly UserManagementApiClient _client;

        /// <summary>
        /// Creates new instance of <see cref="CodeConfirmationService"/>
        /// </summary>
        public CodeConfirmationService()
        {
            this._client = new UserManagementApiClient();
        }

        /// <summary>
        /// Executes the service
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>result</returns>
        public async Task<Response<HttpResponseMessage>> Execute(object parameter)
        {
            return await this._client.VerifyAsync((UserVerificationInfo)parameter);
        }
    }
}
