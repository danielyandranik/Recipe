using Desktop.Interfaces;
using InstitutionClient;
using System.Threading.Tasks;
using UserManagementConsumer.Client;

namespace Desktop.Services
{
    /// <summary>
    /// Abstract type for profile service
    /// </summary>
    public abstract class ProfileService : IService<Response<string>>
    {
        /// <summary>
        /// User management API client
        /// </summary>
        protected readonly UserManagementApiClient userManagementApiClient;

        /// <summary> 
        /// Institutions API client 
        /// </summary>
        protected readonly Client institutionClient;

        /// <summary>
        /// Creates new instance of <see cref="ProfileService"/>
        /// </summary>
        public ProfileService()
        {
            this.userManagementApiClient = ((App)App.Current).UserApiClient;
            this.institutionClient = ((App)App.Current).InstitutionClient;
        }

        /// <summary>
        /// Executes the service
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>result</returns>
        public abstract Task<Response<string>> Execute(object parameter);
    }
}
