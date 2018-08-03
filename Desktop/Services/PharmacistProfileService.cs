using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    /// <summary>
    /// Pharmacist profile service
    /// </summary>
    public class PharmacistProfileService : ProfileService
    {
        /// <summary>
        /// Executes pharmacist profile service operation.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>response</returns>
        public async override Task<Response<string>> Execute(object parameter)
        {
            var pharmacist = (PharmacistFullInfo)parameter;

            var institutionResponse = await this.institutionClient.GetInstitutionIdAsync(pharmacist.PharmacyName);

            if (!institutionResponse.IsSuccessStatusCode)
                return new Response<string>
                {
                    Result = institutionResponse.StatusCode.ToString(),
                    Status = Status.Error
                };

            return await this.userManagementApiClient.PostPharmacistAsync(pharmacist);
        }
    }
}
