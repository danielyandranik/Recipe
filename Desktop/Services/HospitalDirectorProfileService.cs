using System.Linq;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    /// <summary>
    /// Hospital director profile service
    /// </summary>
    class HospitalDirectorProfileService : ProfileService
    {
        /// <summary>
        /// Executes the profile service operation.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>response</returns>
        public async override Task<Response<string>> Execute(object parameter)
        {
            var hospitalDirector = (HospitalDirector)parameter;

            var institutionResponse = await this.institutionClient.GetInstitutionIdAsync(hospitalDirector.HospitalName);

            if (!institutionResponse.IsSuccessStatusCode)
                return new Response<string>
                {
                    Result = institutionResponse.StatusCode.ToString(),
                    Status = Status.Error
                };

            var id = (int)institutionResponse.Content;

            var hospital = await this.institutionClient.GetInstitutionAsync(id);
            hospitalDirector.HospitalName = hospital.Content.Name;

            return await this.userManagementApiClient.PostHospitalDirectorAsync((HospitalDirector)parameter);
        }
    }
}
