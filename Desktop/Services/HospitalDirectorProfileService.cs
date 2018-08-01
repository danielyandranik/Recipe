using System.Linq;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    class HospitalDirectorProfileService : ProfileService
    {
        public async override Task<Response<string>> Execute(object parameter)
        {
            var hospitalDirector = (HospitalDirector)parameter;

            var institutionResponse = await this.institutionClient.GetHospitalsByNameAsync(hospitalDirector.HospitalName);

            if (!institutionResponse.IsSuccessStatusCode)
                return new Response<string>
                {
                    Result = institutionResponse.StatusCode.ToString(),
                    Status = Status.Error
                };

            hospitalDirector.HospitalName = institutionResponse.Content.First().Name;

            return await this.userManagementApiClient.PostHospitalDirectorAsync((HospitalDirector)parameter);
        }
    }
}
