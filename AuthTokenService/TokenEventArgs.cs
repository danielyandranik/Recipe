using System;

namespace AuthTokenService
{
    /// <summary>
    /// Class for Token event argument
    /// </summary>
    public class TokenEventArgs:EventArgs
    {
        /// <summary>
        /// Access token
        /// </summary>
        private readonly string _accessToken;

        /// <summary>
        /// Gets access token
        /// </summary>
        public string AccessToken => this._accessToken;

        /// <summary>
        /// Creates new instance of <see cref="TokenEventArgs"/>
        /// </summary>
        /// <param name="accessToken"></param>
        public TokenEventArgs(string accessToken)
        {
            // setting fields
            this._accessToken = accessToken;
        }

    }
}
