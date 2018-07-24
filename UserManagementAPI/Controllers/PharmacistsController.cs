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
    /// Pharmacists controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/pharmacists")]
    public class PharmacistsController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of PharmacistsController
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        public PharmacistsController(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Gets all pharmacists
        /// </summary>
        /// <returns>action result</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            // getting pharmacists
            var result = await this._dataManager.OperateAsync<Pharmacist>("GetAllPharmacists");

            // returning result
            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        /// <summary>
        /// Gets pharmacist by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            // gettting userId
            var userId = this.GetUserId();

            if (userId != id)
                return new StatusCodeResult(401);

            // getting doctor
            var pharmacist = this._dataManager.Operate<int, PharmacistFullInfo>("GetPharmacistById", id);

            // returning result
            if (pharmacist == null)
                return new StatusCodeResult(404);

            return new JsonResult(pharmacist);
        }

        /// <summary>
        /// Posts new pharmacist
        /// </summary>
        /// <param name="pharmacistFullInfo">pharmacist full info</param>
        /// <returns>action result</returns>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]PharmacistFullInfo pharmacistFullInfo)
        {
            // checking id
            if (pharmacistFullInfo.UserId != this.GetUserId())
                return new StatusCodeResult(401);

            // adding new pharmacist
            var result = (int)this._dataManager.Operate<PharmacistFullInfo, object>("CreatePharmacist", pharmacistFullInfo);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Updates pharmacist
        /// </summary>
        /// <param name="pharmacistFullInfo">pharmacist full info</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]PharmacistFullInfo pharmacistFullInfo)
        {
            // checking id
            if (pharmacistFullInfo.UserId != this.GetUserId())
                return new StatusCodeResult(401);

            // updating pharmacist
            var result = (int)this._dataManager.Operate<PharmacistFullInfo, object>("UpdatePharmacist", pharmacistFullInfo);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Deletes pharmacist
        /// </summary>
        /// <returns>action result</returns>
        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            // getting user id
            var userId = this.GetUserId();

            // deleting
            var result = (int)this._dataManager.Operate<int, object>("DeletePharmacist", userId);

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
