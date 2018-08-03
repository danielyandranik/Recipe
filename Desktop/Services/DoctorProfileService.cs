using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    /// <summary>
    /// Doctor profile service
    /// </summary>
    public class DoctorProfileService : ProfileService
    {
        /// <summary>
        /// Executes profile service operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>response</returns>
        public async override Task<Response<string>> Execute(object parameter)
        {
            var doctor = (Doctor)parameter;

            var institutionResponse = await this.institutionClient.GetInstitutionIdAsync(doctor.HospitalName);

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
