using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    class HospitalDirectorProfileService : ProfileService
    {
        public async override Task<Response<string>> Execute(object parameter)
        {
            return await this.userManagementApiClient.PostHospitalDirectorAsync((HospitalDirector)parameter);
        }
    }
}
