using Desktop.Interfaces;
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
        protected readonly UserManagementApiClient _userManagementApiClient;

        /// <summary>
        /// Creates new instance of <see cref="ProfileService"/>
        /// </summary>
        public ProfileService()
        {
            this._userManagementApiClient = ((App)App.Current).UserApiClient;
        }

        /// <summary>
        /// Executes the service
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>result</returns>
        public abstract Task<Response<string>> Execute(object parameter);
    }
}
