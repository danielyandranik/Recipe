using System.Collections.Generic;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// Register controller
    /// </summary>
    [Produces("application/json")]
    public class RegisterController : Controller
    {
        /// <summary>
        /// Mail service
        /// </summary>
        private MailService _mailService;

        /// <summary>
        /// Verifier service
        /// </summary>
        private Verifier _verifier;

        /// <summary>
        /// Repository
        /// </summary>
        private Repo<UserFullInfo> _repo;

        /// <summary>
        /// Creates new instance of RegisterController
        /// </summary>
        /// <param name="repo">Repository</param>
        /// <param name="mailService">Mail service.</param>
        /// <param name="verifier">Verifier</param>
        public RegisterController(Repo<UserFullInfo> repo,MailService mailService,Verifier verifier)
        {
            // setting fields
            this._mailService = mailService;
            this._verifier = verifier;
            this._repo = repo;
           
        }

        /// <summary>
        /// Operates POST request
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>action result</returns>
        [HttpPost]
        [Route("api/register")]
        public IActionResult Post([FromBody]UserFullInfo user)
        {
            // adding user
            var addedUsers = (int)this._repo.ExecuteOperation("CreateUser", user);

            // if user exists return 'Conflict' code
            if (addedUsers == -1)
                return new StatusCodeResult(409);

            // generating verfification key and sending to user email
            var key = this._verifier.GenerateVerifyKey(user.UserName);
            this._mailService.Send(user.Email, key);

            // returning 200
            return Ok();
        }

        /// <summary>
        /// Operates PUT request
        /// </summary>
        /// <param name="userVerificationInfo">User verification info.</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Route("api/register/verify")]
        public IActionResult Put([FromBody]UserVerificationInfo userVerificationInfo)
        {
            // if verification fails,return 'Bad Request'
            if (!this._verifier.Verify(userVerificationInfo.Username, userVerificationInfo.VerifyKey))
                return new StatusCodeResult(400);

            // otherwise verify
            this._repo.ExecuteOperation("VerifyUser",
                  new[]
                  {
                        new KeyValuePair<string,object>("Username",userVerificationInfo.Username)
                  });

            // returning 200
            return Ok();

        }
    }
}
