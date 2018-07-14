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
        /// Full info repository
        /// </summary>
        private Repo<UserFullInfo> _pRepo;

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
        [Authorize(Policy = "HasProfile")]
        public async Task<IActionResult> Get()
        {
            // getting query
            var query = this.Request.Query;

             // result
            var result = null as object;

            // if no query return all users
            if (query.Count == 0)
            {
                result = await this._repo.ExecuteOperationAsync("GetAllUsers");
            }
            // else return query-specific result
            else if (query.Count == 1)
            {
                if (query.ContainsKey("id"))
                {
                    
                    result = await this._repo.ExecuteOperationAsync("GetUsersPublicInfoById",
                        new[]
                        {
                           new KeyValuePair<string,object>("id",query["id"])
                        });
                }
                else if(query.ContainsKey("username"))
                {
                    result = await this._repo.ExecuteOperationAsync("GetUsersPublicInfoByUsername",
                        new[]
                        {
                            new KeyValuePair<string,object>("username",query["username"])
                        });
                }
            }

            // if no result return Error code
            if (result == null)
                return new StatusCodeResult(404);

            // return result
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
            // getting result
            var result = (int)this._repo.ExecuteOperation("DeleteUser",
                new[]
                {
                    new KeyValuePair<string,object>("id",id)
                });

            // if not deleted return 404
            if (result == -1)
                return new StatusCodeResult(404);

            // returning success code
            return new StatusCodeResult(200);
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