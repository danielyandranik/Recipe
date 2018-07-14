using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using DatabaseAccess.Repository;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Repository
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
        /// Gets enumerable of users
        /// </summary>
        /// <returns>enumerable of users.</returns>
        [HttpGet]
        [Authorize(Policy = "HasProfile")]
        public async Task<IActionResult> Get()
        {
            // getting result
            var result = await this._dataManager
                .OperateAsync<UserPublicInfo>("GetAllUsers");

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        [HttpDelete]
        [Authorize]
        public void Delete()
        {
            // getting id
            var id = this.GetUserId();

            // deleting user
            this._dataManager.Operate<UserPublicInfo>("DeleteUser",
                new[]
                {
                    new KeyValuePair<string,object>("id",id)
                });
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
    }
}