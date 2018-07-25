using System;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace AuthTokenService
{
    /// <summary>
    /// Class for retrieving tokens from Auth API.
    /// This class is also responsible for updating access tokens when they are expired or user claims are changed.
    /// TokenProvider is singleton
    /// </summary>
    public sealed class TokenProvider : IDisposable
    {
        /// <summary>
        /// Token Provider instance
        /// </summary>
        private static TokenProvider instance = null;

        /// <summary>
        /// Lock object
        /// </summary>
        private static readonly object _lockObject = new object();

        /// <summary>
        /// Token client
        /// </summary>
        private readonly TokenClient _tokenClient;

        /// <summary>
        /// Updater
        /// </summary>
        private readonly Updater _updater;

        /// <summary>
        /// Update interval in minutes
        /// </summary>
        private readonly int _updateInterval;

        /// <summary>
        /// Auth API bas address
        /// </summary>
        private readonly string _authApiAddress;

        /// <summary>
        /// Refresh token
        /// </summary>
        private string _refreshToken;

        /// <summary>
        /// Access token
        /// </summary>
        private string _accessToken;

        /// <summary>
        /// Gets refresh token
        /// </summary>
        public string RefreshToken => this._refreshToken;

        /// <summary>
        /// Gets access token
        /// </summary>
        public string AccessToken => this._accessToken;

        /// <summary>
        /// Gets Auth API address
        /// </summary>
        public string AuthApiAddress => this._authApiAddress;

        /// <summary>
        /// Gets Update interval in minutes
        /// </summary>
        public int UpdateInterval => this._updateInterval;

        /// <summary>
        /// Gets singleton instance
        /// </summary>
        public static TokenProvider Instance
        {
            get
            {
                lock(_lockObject)
                {
                    if(instance == null)
                    {
                        instance = new TokenProvider("http://localhost:5700",28);
                    }

                    return instance;
                }
            }
        }

        /// <summary>
        /// Delegate for Token update
        /// </summary>
        /// <typeparam name="TEventArgs">Type of event argument</typeparam>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        public delegate void TokenUpdatedDelegate<TEventArgs>(object sender, TEventArgs e);

        /// <summary>
        /// Event for Token update
        /// </summary>
        public event TokenUpdatedDelegate<TokenEventArgs> TokenUpdated;

        /// <summary>
        /// Creates new instance of <see cref="TokenProvider"/>
        /// </summary>
        /// <param name="authApiAddress">Auth API base address</param>
        /// <param name="updateInterval">Update interval</param>
        private TokenProvider(string authApiAddress,int updateInterval)
        {
            // setting fields
            this._authApiAddress = authApiAddress;
            this._updateInterval = updateInterval;

            // getting token endpoint
            var disco = DiscoveryClient.GetAsync(this.AuthApiAddress).Result;

            if(disco.IsError)
            {
                throw new Exception("Auth API is not responding");
            }

            // creating token client
            this._tokenClient = new TokenClient(disco.TokenEndpoint, "DefaultClient", "secret");

            // creating updater with the given interval
            this._updater = new Updater(this._updateInterval, this.RefreshAccessTokenAsync);
        }

        /// <summary>
        /// Does Sign In operation with password credentials
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>task</returns>
        public async Task SignInAsync(string username,string password)
        {
            // getting response
            var response = await this._tokenClient.RequestResourceOwnerPasswordAsync(
                username,password,
                "offline_access UserManagementAPI MedicineAPI RecipeAPI InstitutionsAPI");
            
            if(response.IsError)
            {
                throw new Exception("Invalid username or password");
            }

            // setting tokens
            this._accessToken = response.AccessToken;
            this._refreshToken = response.RefreshToken;

            // starting update periodic process
            this._updater.StartUpdatePeriod();

            // rising token updated event
            this.RiseTokenUpdatedEvent();
        }

        /// <summary>
        /// Does refresh of access token
        /// </summary>
        /// <returns>task</returns>
        public async Task RefreshAccessTokenAsync()
        {
            // getting response
            var response = await this._tokenClient.RequestRefreshTokenAsync(this._refreshToken);

            if(response.IsError)
            {
                throw new Exception("Invalid refresh token");
            }

            // setting new access token
            this._accessToken = response.AccessToken;

            // rising token updated event
            this.RiseTokenUpdatedEvent();
        }

        /// <summary>
        /// Dispose token provider
        /// </summary>
        public void Dispose()
        {
            this._updater.Dispose();
            this._tokenClient.Dispose();
        }

        /// <summary>
        /// Rises token updated event
        /// </summary>
        private void RiseTokenUpdatedEvent()
        {
            // creating token update event argument
            var tokenUpdateEventArgs = new TokenEventArgs(this._accessToken);

            // rising token updated event
            this.TokenUpdated?.Invoke(this, tokenUpdateEventArgs);
        }
    }
}
