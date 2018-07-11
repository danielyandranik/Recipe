using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using DatabaseAccess.Repository;
using DatabaseAccess.SpExecuters;
using System.Linq;
using System.Security.Claims;

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
        private Repo<UserFullInfo, SpExecuter> _repo;

        /// <summary>
        /// Repository for user public info.
        /// </summary>
        private Repo<UserPublicInfo, SpExecuter> _pRepo;

        /// <summary>
        /// Creates new instance of user controller
        /// </summary>
        public UsersController()
        {
            this._repo = new Repo<UserFullInfo, SpExecuter>("MapInfo\\UserMap.xml");

            this._pRepo = new Repo<UserPublicInfo, SpExecuter>("MapInfo\\UserMap.xml");
        }

        /// <summary>
        /// Gets enumerable of users
        /// </summary>
        /// <returns>enumerable of users.</returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<UserPublicInfo> Get()
        {
            return (IEnumerable<UserPublicInfo>)this._pRepo.ExecuteOperation("GetAllUsers");
        }
        
        /// <summary>
        /// Posts new user
        /// </summary>
        /// <param name="user">User</param>
        [HttpPost]
        public void Post([FromBody]UserFullInfo user)
        {
            this._repo.ExecuteOperation("CreateUser", user);
        }

        /// <summary>
        /// Puts user
        /// </summary>
        /// <param name="user">user</param>
        [HttpPut]
        [Authorize]
        public void Put(int id,[FromBody]UserFullInfo user)
        {
            var userId = int.Parse(
                ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);

            if (id == userId)
            {
                this._repo.ExecuteOperation("UpdateUser", user);
            }
        }

        /// <summary>
        /// Delets user
        /// </summary>
        /// <param name="id">id</param>
        [HttpDelete]
        [Authorize]
        public void Delete(int id)
        {
            var userId = int.Parse(
                ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);

            if (id == userId)
            {
                this._repo.ExecuteOperation("DeleteUser",
                  new[]
                  {
                    new KeyValuePair<string,object>("id",id)
                  });
            }
        }
    }
}
