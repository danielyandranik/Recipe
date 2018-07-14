using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using DatabaseAccess.Repository;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// Users controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of user controller
        /// </summary>
        public UsersController(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Gets  users
        /// </summary>
        /// <returns>users.</returns>
        [HttpGet]
        [Authorize(Policy = "HasProfile")]
        public async Task<IActionResult> Get()
        {
            // getting result
            var result = await this._dataManager
                .OperateAsync<UserPublicInfo>("GetAllUsers");

            // returning result
            return this.GetGetResult(result);
        }

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>user</returns>
        [HttpGet("{id:int}")]
        [Authorize(Policy = "HasProfile")]
        public IActionResult Get(int id)
        {
            // getting id
            var userId = this.GetUserId();

            // result
            var result = null as object;

            // getting result
            if (userId == id)
                result = this._dataManager.Operate<int, UserPersonalInfo>("GetUserPersonalInfoById", id);
            else
                result = this._dataManager.Operate<int, UserPublicInfo>("GetUserPublicInfoById", id);

            // returning result
            return this.GetGetResult(result);
        }

        /// <summary>
        /// Gets user by username.
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user</returns>
        [HttpGet("{username}")]
        [Authorize(Policy = "HasProfile")]
        public IActionResult Get(string username)
        {
            // getting result
            var result = this._dataManager
                .Operate<string, UserPublicInfo>("GetUserPublicInfoByUsername", username);

            // returning result
            return this.GetGetResult(result);
        }
        
        /// <summary>
        /// Updates password 
        /// </summary>
        /// <param name="passwordUpdateInfo">Password update information</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize]
        [Route("password")]
        public IActionResult Put([FromBody]PasswordUpdateInfo passwordUpdateInfo)
        {
            // updating password
            var result = (int)this._dataManager.Operate<PasswordUpdateInfo, object>("UpdatePassword", passwordUpdateInfo);

            // if password is not updated return 400
            if (result == 0)
                return new StatusCodeResult(400);

            // return 200
            return new StatusCodeResult(200);
        }

        [HttpPut]
        [Authorize]
        [Route("profile")]
        public IActionResult Put([FromBody]ProfileUpdateInfo profileUpdateInfo)
        {
            // updating current profile
            var result = (int)this._dataManager
                .Operate<ProfileUpdateInfo, object>("UpdateCurrentProfile",profileUpdateInfo);

            // if profile not found return 404
            if (result == -1)
                return new StatusCodeResult(404);

            // return 200
            return new StatusCodeResult(200);
        }

        /// <summary>
        /// Deletes user by id.Requieres IsAdmin policy.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "IsAdmin")]
        public IActionResult Delete(int id)
        {
            // deleting user
            var result = (int)this._dataManager.Operate<int, object>("DeleteUser", id);

            // returning result
            return this.GetDeletionResult(result);
        }

        /// <summary>
        /// Gets user id.
        /// </summary>
        /// <returns>User id.</returns>
        private int GetUserId()
        {
            // returning id
            return int.Parse(
                ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);
        }

        /// <summary>
        /// Gets DELETE result
        /// </summary>
        /// <param name="result">result</param>
        /// <returns>action result</returns>
        private IActionResult GetDeletionResult(int result)
        {
            // if no such user return 404
            if (result == 0)
                return new StatusCodeResult(404);

            // return 200
            return new StatusCodeResult(200);
        }

        /// <summary>
        /// Gets GET result
        /// </summary>
        /// <param name="result">result</param>
        /// <returns>action result</returns>
        private IActionResult GetGetResult(object result)
        {
            // if there is no such user return 404
            if (result == null)
                return new StatusCodeResult(404);

            // return the result
            return new JsonResult(result);
        }
    }
}