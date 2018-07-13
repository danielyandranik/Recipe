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
        private Repo<UserPublicInfo> _repo;

        /// <summary>
        /// Creates new instance of user controller
        /// </summary>
        public UsersController(Repo<UserPublicInfo> repo)
        {
            this._repo = repo;
        }

        /// <summary>
        /// Gets enumerable of users
        /// </summary>
        /// <returns>enumerable of users.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var result = await this._repo.ExecuteOperationAsync("GetAllUsers");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);                
        }
        
        [HttpGet("{id:int}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var user = this._repo.ExecuteOperation("GetUserPublicInfoById",
                new[]
                {
                    new KeyValuePair<string,object>("Id",id)
                });

            if (user == null)
                return new StatusCodeResult(404);

            return new JsonResult(user);
        }

        [HttpGet("{username}")]
        [Authorize]
        public IActionResult Get(string username)
        {
            var user = this._repo.ExecuteOperation("GetUserPublicInfoByUsername",
                new[]
                {
                    new KeyValuePair<string,object>("Username",username)
                });

            if (user == null)
                return new StatusCodeResult(404);

            return new JsonResult(user);
        }

        /// <summary>
        /// Delets user
        /// </summary>
        [HttpDelete]
        [Authorize]
        public void Delete()
        {
            var id = this.GetUserId();

            this._repo.ExecuteOperation("DeleteUser",
                new[]
                {
                    new KeyValuePair<string,object>("id",id)
                });
        }

        /// <summary>
        /// Deletes user by id.Demands 'IsAdmin' policy.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>returns action result.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "IsAdmin")]
        public IActionResult Delete(int id)
        {
            var result = (int)this._repo.ExecuteOperation("DeleteUser",
                new[]
                {
                    new KeyValuePair<string,object>("id",id)
                });

            if (result == -1)
                return new StatusCodeResult(404);

            return new StatusCodeResult(200);
        }

        private int GetUserId()
        {
            return int.Parse(
                ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);
        }
    }
}
