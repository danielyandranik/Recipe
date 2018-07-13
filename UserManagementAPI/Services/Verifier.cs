using System;
using System.Collections.Generic;

namespace UserManagementAPI.Services
{
    /// <summary>
    /// Verifier for user
    /// </summary>
    public class Verifier
    {
        /// <summary>
        /// Verification keys
        /// </summary>
        private Dictionary<string, string> _verifyKeys;

        /// <summary>
        /// Creates new instance of Verifier
        /// </summary>
        public Verifier()
        {
            this._verifyKeys = new Dictionary<string, string>();
        }

        /// <summary>
        /// Generates verification key
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>verifiation key</returns>
        public string GenerateVerifyKey(string username)
        {
            var random = new Random();
            var result = "";

            // genarting verification key
            // this is not very secure 
            // here we need changes
            for(var counter = 0; counter < 32; counter++)
            {
                var num = random.Next(65, 121);

                if (num > 90 && num < 97)
                    num += 10;

                result += (char)num;
            }

            this._verifyKeys[username] = result;

            return result;
        }

        /// <summary>
        /// Verifies the user.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="verifyKey">Verification key</param>
        /// <returns>Verifiaction logic result</returns>
        public bool Verify(string username, string verifyKey)
        {
            if (!this._verifyKeys.ContainsKey(username))
                return false;

            if (this._verifyKeys[username] == verifyKey)
            {
                this._verifyKeys.Remove(username);
                return true;
            }
            
            return false;            
        }
    }
}
