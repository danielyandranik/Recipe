using Microsoft.AspNetCore.Mvc;
using DatabaseAccess.Repository;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// Register controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/register")]
    public class RegisterController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Verifier
        /// </summary>
        private Verifier _verifier;

        /// <summary>
        /// Password hash service
        /// </summary>
        private PasswordHashService _passwordHS;

        /// <summary>
        /// Creates Register Controller
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        /// <param name="verifier">Verifier</param>
        /// <param name="passwordHashService">Password Hash Sevice</param>
        public RegisterController(DataManager dataManager,Verifier verifier,PasswordHashService passwordHashService)
        {
            // setting fields
            this._dataManager = dataManager;
            this._verifier = verifier;
            this._passwordHS = passwordHashService;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="userRegisterInfo">User registration information</param>
        /// <returns>Action result</returns>
        [HttpPost]
        public IActionResult Post([FromBody]UserRegisterInfo userRegisterInfo)
        {
            // hashing password
            userRegisterInfo.Password = this._passwordHS.HashPassword(userRegisterInfo.Password);

            // register user
            var result = (int)this._dataManager.Operate<UserRegisterInfo, object>("CreateUser", userRegisterInfo);
             
            // if registration is not successful return Bad Request code
            if (result == -1)
                return new StatusCodeResult(400);

            // adding verification information
            this._verifier.AddVerificationInfo(userRegisterInfo.Username, userRegisterInfo.Email);

            // return Success code
            return new StatusCodeResult(200);
        }

        /// <summary>
        /// Verifies user
        /// </summary>
        /// <param name="userVerificationInfo">User verifaction information</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Route("verify")]
        public IActionResult Put([FromBody]UserVerificationInfo userVerificationInfo)
        {
            // if user verification succeeds return 200
            if (this._verifier.Verify(userVerificationInfo))
                return new StatusCodeResult(200);

            // return Bad Request code
            return new StatusCodeResult(400);
        }
    }
}
