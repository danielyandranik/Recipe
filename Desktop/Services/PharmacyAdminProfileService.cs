using System.Linq;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    public class PharmacyAdminProfileService : ProfileService
    {
        public async override Task<Response<string>> Execute(object parameter)
        {
            var pharmacyAdmin = (PharmacyAdmin)parameter;

            var institutionResponse = await institutionClient.GetPharmaciesByNameAsync(pharmacyAdmin.PharmacyName);

            if (!institutionResponse.IsSuccessStatusCode)
                return new Response<string>
                {
                    Result = institutionResponse.StatusCode.ToString(),
                    Status = Status.Error
                };

            pharmacyAdmin.PharmacyId = institutionResponse.Content.First().Id;

            return await this.userManagementApiClient.PostPharmacyAdmin(pharmacyAdmin);
        }
    }
}
