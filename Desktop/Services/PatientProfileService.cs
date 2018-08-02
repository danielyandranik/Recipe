using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    /// <summary>
    /// Patient profile service
    /// </summary>
    public class PatientProfileService : ProfileService
    {
        /// <summary>
        /// Executes patient profile service operation.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        /// <returns>response</returns>
        public async override Task<Response<string>> Execute(object parameter)
        {
            return await this.userManagementApiClient.PostPatientAsync((Patient)parameter);
        }
    }
}
