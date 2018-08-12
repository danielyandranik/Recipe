using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// Pharmacy Admins controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/pharmacy-admins")]
    public class PharmacyAdminsController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of <see cref="PharmacyAdminsController"/>
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        public PharmacyAdminsController(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Gets all pharmacy directors
        /// </summary>
        /// <returns>action result</returns>
        [HttpGet]
        [Authorize(Policy = "AdminOrMinistryWorker")]
        public async Task<IActionResult> Get()
        {
            var query = this.Request.Query;
            object result;

            if (query.Count > 0)
            {
                if (query.TryGetValue("isApproved", out StringValues value))
                {
                    if (value.ToString() == "false")
                    {
                        result = await this._dataManager.OperateAsync<UnapprovedPharmacyAdmin>("GetUnapprovedPharmacyAdmins");

                        if (result == null)
                            return new StatusCodeResult(204);

                        return new JsonResult(result);
                    }

                    return new StatusCodeResult(500);
                }

                return new StatusCodeResult(404);
            }

            return new StatusCodeResult(404);
        }

        /// <summary>
        /// Gets all pharmacy directors
        /// </summary>
        /// <returns>action result</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> Get(int id)
        {
            

            return new StatusCodeResult(404);
        }

        /// <summary>
        /// Posts new pharmacy admin
        /// </summary>
        /// <param name="pharmacyAdmin">Pharmacy admin</param>
        /// <returns>action result</returns>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] PharmacyAdmin pharmacyAdmin)
        {
            // checking id
            if (this.GetUserId() != pharmacyAdmin.UserId)
                return new StatusCodeResult(401);

            // adding new pharmacy admin
            var result = (int)this._dataManager.Operate<PharmacyAdmin, object>("CreatePharmacyAdmin", pharmacyAdmin);

            // returning action result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Deletes pharmacy admin
        /// </summary>
        /// <returns>action result</returns>
        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            // getting id
            var id = this.GetUserId();

            // getting deletion result
            var result = (int)this._dataManager.Operate<int, object>("DeletePharmacyAdmin", id);

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
        /// Gets action result
        /// </summary>
        /// <param name="result">result</param>
        /// <returns>return result</returns>
        private IActionResult GetActionResult(int result)
        {
            if (result == 0 || result == -1)
                return new StatusCodeResult(400);

            return new StatusCodeResult(200);
        }
    }
}
