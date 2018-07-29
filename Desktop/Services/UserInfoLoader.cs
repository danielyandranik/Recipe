using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using Desktop.Models;
using UserManagementConsumer.Client;

namespace Desktop.Services
{
    public class UserInfoLoader 
    {
        private readonly UserManagementApiClient _client;

        public UserInfoLoader()
        {
            this._client = ((App)App.Current).UserApiClient;
        }

        public async Task<UserInitialInfo> Execute()
        {
            var response = await this._client.GetUserByUsernameAsync(User.Default.Username);

            if (response.Status == Status.Error)
                return null;

            var profilesResponse = await this._client.GetUserProfilesAsync(User.Default.Id);
            
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
