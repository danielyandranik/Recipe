using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    public class DoctorProfileService : ProfileService
    {
        public async override Task<Response<string>> Execute(object parameter)
        {
            return await this.userManagementApiClient.PostDoctorAsync((Doctor)parameter);
        }
    }
}
