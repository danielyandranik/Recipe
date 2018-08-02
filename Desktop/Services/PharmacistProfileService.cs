using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    public class PharmacistProfileService : ProfileService
    {
        public async override Task<Response<string>> Execute(object parameter)
        {
            var pharmacist = (PharmacistFullInfo)parameter;

            var institutionResponse = await this.institutionClient.GetPharmaciesByNameAsync(pharmacist.PharmacyName);

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
