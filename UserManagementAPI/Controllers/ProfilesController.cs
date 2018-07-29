using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// Controller for profiles
    /// </summary>
    [Produces("application/json")]
    [Route("api/profiles")]
    public class ProfilesController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of profiles controller.
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        public ProfilesController(DataManager dataManager)
        {
            // setting fields
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns>action result</returns>
        [HttpGet]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Get()
        {
            //getting result
            var result = await this._dataManager.OperateAsync<Profile>("GetAllProfiles");

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Gets profiles by user id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            if (this.GetUserId() != id)
                return new StatusCodeResult(403);

            // getting result
            var result = await this._dataManager.OperateAsync<int, Profile>("GetProfilesById", id);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Gets profiles by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>action result</returns>
        [HttpGet("{username}")]
        [Authorize]
        public async Task<IActionResult> Get(string username)
        {
            if (this.GetUsername() != username)
                return new StatusCodeResult(403);

            // getting result
            var result = await this._dataManager.OperateAsync<string, Profile>("GetProfilesByUsername", username);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Gets unapproved profiles by type.
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>action result</returns>
        [HttpGet("{type}")]
        [Authorize(Policy = "IsApprover")]
        [Route("unapproved")]
        public async Task<IActionResult> GetAUnapproved(string type)
        {
            // getting result
            var result = await this._dataManager.OperateAsync<string, Profile>("GetUnapprovedProfiles", type);

            // returning result
            return this.GetActionResult(result);
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
        /// Gets username
        /// </summary>
        /// <returns>username</returns>
        private string GetUsername()
        {
            // returning username
            return ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "name").First().Value;
        }

        /// <summary>
        /// Gets action result
        /// </summary>
        /// <param name="result">result</param>
        /// <returns>action result</returns>
        public IActionResult GetActionResult(object result)
        {
            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }
    }
}