using System.Linq;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using Desktop.Models;

namespace Desktop.Services
{
    /// <summary>
    /// User info loader
    /// </summary>
    public class UserInfoLoader 
    {
        /// <summary>
        /// User Management API client
        /// </summary>
        private readonly UserManagementApiClient _client;

        /// <summary>
        /// Creates new instance of <see cref="UserInfoLoader"/>
        /// </summary>
        public UserInfoLoader()
        {
            // setting fields
            this._client = ((App)App.Current).UserApiClient;
        }

        /// <summary>
        /// Executes User info loader service operation
        /// </summary>
        /// <returns>user initial information</returns>
        public async Task<UserInitialInfo> Execute()
        {
            var response = await this._client.GetUserAsync(User.Default.Username);

            if (response.Status == Status.Error)
                return null;

            var profilesResponse = await this._client.GetUserProfilesAsync(User.Default.Username);
            
            return new UserInitialInfo
            {
                Id = response.Result.Id,
                CurrentProfile = response.Result.CurrentProfileType,
                FullName = $"{response.Result.FirstName} {response.Result.LastName}",
                Username = response.Result.Username,
                Profiles = profilesResponse.Result?.Select(profile => profile.Type)
            };
        }
    }
}
