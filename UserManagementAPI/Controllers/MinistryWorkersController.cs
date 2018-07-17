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
    /// Controller for ministry workers.
    /// </summary>
    [Produces("application/json")]
    [Route("api/ministryworkers")]
    public class MinistryWorkersController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of data manager.
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        public MinistryWorkersController(DataManager dataManager)
        {
            // setting fields
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Gets all ministry workers
        /// </summary>
        /// <returns>action result</returns>
        [HttpGet]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Get()
        {
            // getting result
            var result = await this._dataManager.OperateAsync<MinistryWorker>("GetAllMinistryWorkers");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        /// <summary>
        /// Gets ministry worker by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            if (this.GetUserId() != id)
                return new StatusCodeResult(401);

            // getting result
            var result = this._dataManager.Operate<int, MinistryWorker>("GetMinistryWorkerById", id);

            // returning result
            return new JsonResult(result);
        }

        /// <summary>
        /// Posts new ministry worker
        /// </summary>
        /// <param name="ministryWorker">ministry worker</param>
        /// <returns>action result</returns>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]MinistryWorker ministryWorker)
        {
            // getting operation result
            var result = (int)this._dataManager
                .Operate<int,MinistryWorker,object>("CreateMinistryWorker", this.GetUserId(), ministryWorker);

            // return result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Puts ministry worker
        /// </summary>
        /// <param name="ministryWorker">ministry worker</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]MinistryWorker ministryWorker)
        {
            // getting operation result
            var result = (int)this._dataManager
                .Operate<int, MinistryWorker, object>("UpdateMinistryWorker", this.GetUserId(), ministryWorker);

            // return result
            return this.GetActionResult(result);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            // getting operation result
            var result = (int)this._dataManager.Operate<int, object>("DeleteMinistryWorker", this.GetUserId());

            // return result
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
        /// <returns>action result</returns>
        private IActionResult GetActionResult(int result)
        {
            if (result == 0)
                return new StatusCodeResult(404);

            return new StatusCodeResult(200);
        }
    }
}
