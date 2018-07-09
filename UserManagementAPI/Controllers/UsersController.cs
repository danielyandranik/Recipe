using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatabaseAccess;
using UserManagementAPI.Models;

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
        /// Stored procedure executer
        /// </summary>
        private readonly SpExecuter spExecuter;

        /// <summary>
        /// Creates new instance of user controller
        /// </summary>
        public UsersController()
        {
            this.spExecuter = new SpExecuter("(local)","UsersDB", true);
        }

        /// <summary>
        /// Gets enumerable of users
        /// </summary>
        /// <returns>enumerable of users.</returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<UserPublicInfo> Get()
        {
            return this.spExecuter.ExecuteSp<UserPublicInfo>("uspGetAllUsers");
        }
        
        /// <summary>
        /// Posts new user
        /// </summary>
        /// <param name="user">User</param>
        [HttpPost]
        public void Post([FromBody]UserFullInfo user)
        {
            this.spExecuter.ExecuteSpNonQuery(
                "uspCreateUser",
                new[]
                {
                    new KeyValuePair<string,object>("FirstName",user.FirstName),
                    new KeyValuePair<string, object>("MiddleName",user.MiddleName),
                    new KeyValuePair<string,object>("LastName",user.LastName),
                    new KeyValuePair<string,object>("Birthdate",user.Birthdate),
                    new KeyValuePair<string, object>("Sex",user.Sex),
                    new KeyValuePair<string, object>("Email",user.Email),
                    new KeyValuePair<string, object>("Password",user.Password),
                    new KeyValuePair<string, object>("Phone",user.Phone),
                    new KeyValuePair<string, object>("userName",user.UserName)
                }); 
        }
    }
}
