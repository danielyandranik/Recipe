using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    public class PharmacistProfileService : ProfileService
    {
        public async override Task<Response<string>> Execute(object parameter)
        {
            return await userManagementApiClient.PostPharmacistAsync((PharmacistFullInfo)parameter);
        }
    }
}
