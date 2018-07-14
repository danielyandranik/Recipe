using System;
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
            var key = this.GenerateVerifyKey();

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
        /// </summary>
        /// <returns>Verification key</returns>
        private string GenerateVerifyKey()
        {
            var random = new Random();
            var key = "";

            // generating verification key
            // this is not very secure 
            // here we need changes
            for(var counter = 0; counter < 32; counter++)
            {
                var num = random.Next(65, 121);

                if (num > 90 && num < 97)
                    num += 10;

                key += (char)num;
            }

            // returning key
            return key;
        }
    }
}
