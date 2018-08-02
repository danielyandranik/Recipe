using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    public class DoctorProfileService : ProfileService
    {
        public async override Task<Response<string>> Execute(object parameter)
        {
            var doctor = (Doctor)parameter;

            var institutionResponse = await this.institutionClient.GetHospitalsByNameAsync(doctor.HospitalName);

            if (!institutionResponse.IsSuccessStatusCode)
                return new Response<string>
                {
                    Result = institutionResponse.StatusCode.ToString(),
                    Status = Status.Error
                };

            return await this.userManagementApiClient.PostDoctorAsync(doctor);
        }
    }
}
