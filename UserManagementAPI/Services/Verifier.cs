using System;
using System.Security.Cryptography;
using DatabaseAccess.Repository;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    /// <summary>
    /// Verifier for user
    /// </summary>
    public class Verifier
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Mail service
        /// </summary>
        private MailService _mailService;

        /// <summary>
        /// Creates new instance of <see cref="Verifier"/>
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        /// <param name="mailService">Mail service</param>
        public Verifier(DataManager dataManager,MailService mailService)
        {
            // setting fields
            this._dataManager = dataManager;
            this._mailService = mailService;
        }

        /// <summary>
        /// Adds verification info to database
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="mail">Mail address to which verification key must be sent.</param>
        public void AddVerificationInfo(string username,string mail)
        {
            // generating key
            var key = this.GenerateVerifyKey(32);

            // constructing verification info
            var verificationInfo = new UserVerificationInfo
            {
                Username = username,
                VerifyKey = key
            };

            // adding verification key
            this._dataManager.Operate<UserVerificationInfo, object>("AddVerificationKey", verificationInfo);

            // sending verifiaction key to mail
            this._mailService.Send(mail, key);
        }

        /// <summary>
        /// Verifies the user if key is correct.
        /// </summary>
        /// <param name="userVerificationInfo">User verification information</param>
        /// <returns>true,if user is verified,and false otherwise</returns>
        public bool Verify(UserVerificationInfo userVerificationInfo)
        {
            // trying to verify user
            var result = (int)this._dataManager.Operate<UserVerificationInfo, object>("VerifyUser", userVerificationInfo);

            // if user verification fails return false
            if (result == 0)
                return false;

            // return true
            return true;
        }

        /// <summary>
        /// Generates verification key.
        /// Note that random key is now cryptographically more secure than in the previous version,
        /// where System Random class is used.
        /// </summary>
        /// <param name="length">Length of key</param>
        /// <returns>Verification key</returns>
        private string GenerateVerifyKey(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                // buffer for storing random bytes
                var buffer = new byte[length];

                // getting random bytes
                rng.GetBytes(buffer);

                // converting to string
                return Convert.ToBase64String(buffer);
            }
        }
    }
}
